using Google.Protobuf;
using Microsoft.EntityFrameworkCore;
using Quartz;
using Water.Web.Models;

namespace Water.Web.Jobs
{
    public class StartJob : IJob
    {
        public const string SessionIdKey = "SessionId";
        public const string MessageKey = "Message";

        private readonly WaterDbContext dbContext;
        private readonly ILogger<StartJob> logger;
        private readonly ISchedulerFactory schedulerFactory;

        public StartJob(WaterDbContext dbContext, ILogger<StartJob> logger,ISchedulerFactory schedulerFactory)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.schedulerFactory = schedulerFactory;
        }

        public async Task Execute(IJobExecutionContext context)
        {
            var id = (uint)context.MergedJobDataMap.Get(SessionIdKey);
            var msg = (RequestRequest)context.MergedJobDataMap.Get(MessageKey);
            var session = await dbContext.Sessions.FirstOrDefaultAsync(x => x.Id == id);
            if (session == null)
            {
                logger.LogWarning("Can't find session id is {id}", id);
                return;
            }
            session.State = WaterSessionState.Running;
            await dbContext.SaveChangesAsync();
            await CharServiceImpl.SendAsync(MsgType.Start, msg, logger);
            await EndOfSessionJob.StartEndJobAsync(schedulerFactory, msg);
        }
    }
}
