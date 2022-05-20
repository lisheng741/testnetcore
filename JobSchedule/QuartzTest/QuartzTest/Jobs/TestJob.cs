using Quartz;

namespace QuartzTest.Jobs;

public class TestJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        Console.WriteLine("测试 Job --- ");
        return Task.CompletedTask;
    }
}
