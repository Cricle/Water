using Microsoft.EntityFrameworkCore;
using Quartz;
using Water.Web.Models;

namespace Water.Web.Jobs
{
    public class EndOfSessionJob : IJob
    {
        private readonly WaterDbContext dbContext;
        private readonly ILogger<StartJob> logger;

        public EndOfSessionJob(WaterDbContext dbContext, ILogger<StartJob> logger)
        {
            this.dbContext = dbContext;
            this.logger = logger;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var id = (uint)context.MergedJobDataMap.Get(StartJob.SessionIdKey);
            var msg = (RequestRequest)context.MergedJobDataMap.Get(StartJob.MessageKey);
            var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == id);
            if (session == null)
            {
                logger.LogWarning("Can't find session id is {id}", id);
                return;
            }
            session.State = WaterSessionState.End;
            await dbContext.SaveChangesAsync();
            var joined=await dbContext.SessionItems.Where(x=>x.SessionId==msg.ResultId).Select(x=>x.Name).ToListAsync();
            var end = new EndRequestData { Request = msg };
            end.Joined.AddRange(joined);
            await CharServiceImpl.SendAsync(MsgType.EndOfSession, end, logger);
        }
        public static async Task StartEndJobAsync(ISchedulerFactory schedulerFactory, RequestRequest msg)
        {
            var endAt = msg.StartTime.ToDateTime().ToLocalTime().Add(msg.Deadline.ToTimeSpan());
            var job = JobBuilder.Create<EndOfSessionJob>()
                .RequestRecovery()
                    .SetJobData(new JobDataMap
                    {
                        [StartJob.MessageKey] = msg,
                        [StartJob.SessionIdKey] = msg.ResultId
                    })
                .Build();
            var trigger = TriggerBuilder.Create()
                .StartAt(endAt)
                .Build();
            var scheduler = await schedulerFactory.GetScheduler();
            await scheduler.ScheduleJob(job, trigger);
        }
    }
}
