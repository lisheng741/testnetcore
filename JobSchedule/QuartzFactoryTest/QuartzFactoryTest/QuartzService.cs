using Quartz;

namespace QuartzFactoryTest;

public class QuartzService
{
    private readonly ISchedulerFactory _schedulerFactory;

    public QuartzService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task AddJob(string jobName, string cron)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

        await scheduler.Start();
        var jobKey = new JobKey(jobName);
        var job = JobBuilder.Create<TestJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData("name", jobName)
                    .Build();
        var trigger = TriggerBuilder.Create()
                    .ForJob(jobKey)
                    .WithCronSchedule(cron)
                    .Build();
        await scheduler.ScheduleJob(job, trigger);
    }

    public async Task RemoveJob(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

        var jobKey = new JobKey(jobName);
        await scheduler.DeleteJob(jobKey);
    }
}
