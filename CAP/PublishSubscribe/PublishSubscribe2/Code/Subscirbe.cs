namespace PublishSubscribe.Code;

public interface ISubscriber
{
    string EventName { get; }
    Task ExecuteAsync(TestEvent testEvent);
}

public class Subscirbe1EventHandler : ISubscriber
{
    public string EventName { get; } = "test1";

    public Task ExecuteAsync(TestEvent testEvent)
    {
        throw new Exception("测试抛出异常");
        Console.WriteLine($"EventName：{EventName} Name: {testEvent.Name}");
        Console.WriteLine($"EventName：{EventName} Message: {testEvent.Message}");
        return Task.CompletedTask;
    }
}
public class Subscirbe2EventHandler : ISubscriber
{
    public string EventName { get; } = "test2";

    public Task ExecuteAsync(TestEvent testEvent)
    {
        Console.WriteLine($"EventName：{EventName} Name: {testEvent.Name}");
        Console.WriteLine($"EventName：{EventName} Message: {testEvent.Message}");
        return Task.CompletedTask;
    }
}
