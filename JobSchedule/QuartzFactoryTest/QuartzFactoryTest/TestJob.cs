using Quartz;

namespace QuartzFactoryTest;

public class TestJob : IJob
{
    private readonly ILogger<TestJob> _logger;

    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
    }

    public Task Execute(IJobExecutionContext context)
    {
        var map = context.JobDetail.JobDataMap;
        var jobName = map.GetString("name");

        _logger.LogInformation($"{jobName} in {Thread.CurrentThread.ManagedThreadId} run at {DateTime.Now}");
        Console.WriteLine($"{jobName} in {Thread.CurrentThread.ManagedThreadId} run at {DateTime.Now}");

        return Task.CompletedTask;
    }
}
