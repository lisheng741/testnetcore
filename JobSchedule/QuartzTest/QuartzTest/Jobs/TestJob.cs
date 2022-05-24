using Quartz;

namespace QuartzTest.Jobs;

[DisallowConcurrentExecution]
public class TestJob : IJob
{
    private readonly ILogger<TestJob> _logger;
    //private readonly ILogger _logger1;

    public TestJob(ILogger<TestJob> logger)
    {
        _logger = logger;
        //_logger1 = logger1;
    }

    public Task Execute(IJobExecutionContext context)
    {
        _logger.LogInformation("测试 Job --- " + DateTime.Now);
        //_logger1.LogInformation("测试 Job --- " + DateTime.Now);
        Console.WriteLine("测试 Job --- " + DateTime.Now);
        return Task.CompletedTask;
    }
}
