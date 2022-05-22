using Quartz;

namespace QuartzTest.Jobs;

[DisallowConcurrentExecution]
public class TestJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("测试 Job --- " + DateTime.Now);
        return Task.CompletedTask;
    }
}
