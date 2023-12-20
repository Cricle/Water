using Google.Protobuf;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Water.Web.Jobs;
using Water.Web.Models;

namespace Water.Web
{
    public class CharServiceImpl : CharService.CharServiceBase
    {
        internal static readonly List<StreamMessage<MsgResponse>> msgResponseChannel = new List<StreamMessage<MsgResponse>>();
        internal static readonly object locker = new object();

        private readonly WaterDbContext dbContext;
        private readonly ILogger<CharServiceImpl> logger;
        private readonly ISchedulerFactory schedulerFactory;

        public CharServiceImpl(WaterDbContext dbContext, ILogger<CharServiceImpl> logger, ISchedulerFactory schedulerFactory)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.schedulerFactory = schedulerFactory;
        }
        public override async Task<GetSessionResponse> GetSession(GetSessionRequest request, ServerCallContext context)
        {
            var session = await dbContext.Sessions.Where(x => x.Id == request.ResultId).FirstOrDefaultAsync();
            if (session == null)
            {
                return new GetSessionResponse();
            }
            var joined = await dbContext.SessionItems.Where(x => x.SessionId == request.ResultId).Select(x => x.Name).ToListAsync();
            var rep = new GetSessionResponse
            {
                Id = session.Id,
                Owner = session.Owner,
                WithText = session.WithText,
                StartTime = Timestamp.FromDateTime(session.StartTime.ToUniversalTime()),
                State = (GWaterSessionState)session.State
            };
            rep.Joined.AddRange(joined);
            return rep;
        }
        public override async Task<RequestResponse> Start(RequestRequest request, ServerCallContext context)
        {
            var startAt = request.StartTime.ToDateTime().ToLocalTime();
            var isFeature = startAt >= DateTime.Now;
            var session = new WaterSession
            {
                Owner = request.Name,
                State = isFeature ? WaterSessionState.Waiting : WaterSessionState.Running,
                StartTime = request.StartTime.ToDateTime(),
                WithText = request.WithText,
                Dealline=request.Deadline.ToTimeSpan(),
                Items = new[]
                {
                    new WaterSessionItem
                    {
                         Name = request.Name,
                    }
                }
            };
            dbContext.Sessions.Add(session);
            await dbContext.SaveChangesAsync(context.CancellationToken);
            request.ResultId = session.Id;
            if (!isFeature)
            {
                await SendAsync(MsgType.Start, request, logger);
                await EndOfSessionJob.StartEndJobAsync(schedulerFactory, request);
            }
            else
            {
                var job = JobBuilder.Create<StartJob>()
                    .RequestRecovery()
                    .SetJobData(new JobDataMap
                    {
                        [StartJob.MessageKey] = request,
                        [StartJob.SessionIdKey] = session.Id
                    })
                    .Build();
                var trigger = TriggerBuilder.Create()
                    .StartAt(startAt)
                    .Build();
                var scheduler = await schedulerFactory.GetScheduler();
                await scheduler.ScheduleJob(job, trigger);
            }
            return new RequestResponse { ResultId = session.Id };
        }
        public override async Task<CancelResponse> Cancel(CancelRequest request, ServerCallContext context)
        {
            using (var trans = await dbContext.Database.BeginTransactionAsync(context.CancellationToken))
            {
                var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == request.ResultId);
                if (session == null)
                {
                    return new CancelResponse { ResultId = request.ResultId, Result = false };
                }
                session.State = WaterSessionState.Cancel;
                await dbContext.SaveChangesAsync();
                await trans.CommitAsync(context.CancellationToken);
            }
            await SendAsync(MsgType.Cancel, request, logger);
            return new CancelResponse { Result = true, ResultId = request.ResultId };
        }
        public override async Task<AcceptOrRefuseResult> Accept(AcceptOrRefuseRequest request, ServerCallContext context)
        {
            var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == request.ResultId);
            if (session == null)
            {
                return new AcceptOrRefuseResult { ResultId = request.ResultId, ResultType = AcceptResultType.NoSession };
            }
            var hasAccepted = await dbContext.SessionItems.AnyAsync(x => x.SessionId == request.ResultId && x.Name == request.Name);
            if (hasAccepted)
            {
                return new AcceptOrRefuseResult { ResultId = request.ResultId, ResultType = AcceptResultType.AlreadyJoined };
            }
            dbContext.SessionItems.Add(new WaterSessionItem { Name = request.Name, SessionId = request.ResultId });
            await dbContext.SaveChangesAsync();
            var totalCount = await dbContext.SessionItems.Where(x => x.SessionId == request.ResultId).CountAsync();
            await SendAsync(MsgType.Accept, request, logger);
            return new AcceptOrRefuseResult { ResultId = request.ResultId, Name = request.Name, ResultType = AcceptResultType.Succeed, TotalCount = totalCount };
        }
        public override async Task<AcceptOrRefuseResult> Refuse(AcceptOrRefuseRequest request, ServerCallContext context)
        {
            var totalCount = await dbContext.SessionItems.Where(x => x.SessionId == request.ResultId).CountAsync();
            await SendAsync(MsgType.Refuse, request, logger);
            return new AcceptOrRefuseResult { Name = request.Name, ResultId = request.ResultId, ResultType = AcceptResultType.Succeed, TotalCount = totalCount };
        }
        internal static Task SendAsync(MsgType type, IMessage message, ILogger logger)
        {
            return SendAsync(new MsgResponse { Type = type, Body = ByteString.CopyFrom(message.ToByteArray()) }, logger);
        }
        internal static async Task SendAsync(MsgResponse response, ILogger logger)
        {
            for (int i = 0; i < msgResponseChannel.Count; i++)
            {
                try
                {
                    await msgResponseChannel[i].Writer.WriteAsync(response);
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.ToString());
                }
            }
        }
        public override async Task Msg(EmptyRequest request, IServerStreamWriter<MsgResponse> responseStream, ServerCallContext context)
        {
            var ms = new StreamMessage<MsgResponse>();
            lock (locker)
            {
                msgResponseChannel.Add(ms);
            }
            try
            {
                await foreach (var item in ms.WithCancellation(context.CancellationToken))
                {
                    await responseStream.WriteAsync(item);
                }
            }
            finally
            {
                lock (locker)
                {
                    ms.Writer.Complete();
                    msgResponseChannel.Remove(ms);
                }
            }
        }
    }
}
