using Quartz;

public class TestJob : IJob
{
    public Task Execute(IJobExecutionContext context)
    {
        JobDataMap dataMap = context.JobDetail.JobDataMap;

        var value = dataMap.GetString("testKey");

        System.Console.WriteLine("Job 执行：" + DateTime.Now);
        return Task.CompletedTask;
    }
}