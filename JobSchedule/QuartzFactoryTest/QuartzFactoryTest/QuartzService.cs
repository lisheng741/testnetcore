using Quartz;

namespace QuartzFactoryTest;

public class QuartzService
{
    private readonly ISchedulerFactory _schedulerFactory;

    public QuartzService(ISchedulerFactory schedulerFactory)
    {
        _schedulerFactory = schedulerFactory;
    }

    public async Task Start()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
        await scheduler.Start();
    }

    public async Task Shutdown()
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
        await scheduler.Shutdown();
    }

    public async Task AddJob(string jobName, string cron)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

        var jobKey = new JobKey(jobName);
        var job = JobBuilder.Create<TestJob>()
                    .WithIdentity(jobKey)
                    .UsingJobData("name", jobName)
                    .Build();
        var trigger = TriggerBuilder.Create()
                    .ForJob(jobKey)
                    .WithIdentity(jobName)
                    .WithCronSchedule(cron)
                    .Build();
        await scheduler.ScheduleJob(job, trigger);
        Console.WriteLine($"Add job {jobName}");
    }

    public async Task RemoveJob(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();
        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));

        var jobKey = new JobKey(jobName);
        await scheduler.DeleteJob(jobKey);
        Console.WriteLine($"Delete job {jobName}");
    }

    public async Task PauseJob(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
        var jobKey = new JobKey(jobName);

        await scheduler.PauseJob(jobKey);
        Console.WriteLine($"Pause job {jobName}");
    }

    public async Task ResumeJob(string jobName)
    {
        var scheduler = await _schedulerFactory.GetScheduler();

        if (scheduler == null) throw new ArgumentNullException(nameof(scheduler));
        var jobKey = new JobKey(jobName);
        
        await scheduler.ResumeJob(jobKey);
        Console.WriteLine($"Resume job {jobName}");
    }
}
