namespace PublishSubscribe.Code;

public class Subscirbe1EventHandler : IEventHandler
{
    public Task ExecuteAsync(TestEvent testEvent)
    {
        throw new Exception("测试抛出异常");
        Console.WriteLine($"Name1: {testEvent.Name}");
        Console.WriteLine($"Message1: {testEvent.Message}");
        return Task.CompletedTask;
    }
}
public class Subscirbe2EventHandler : IEventHandler
{
    public Task ExecuteAsync(TestEvent testEvent)
    {
        Console.WriteLine($"Name2: {testEvent.Name}");
        Console.WriteLine($"Message2: {testEvent.Message}");
        return Task.CompletedTask;
    }
}
