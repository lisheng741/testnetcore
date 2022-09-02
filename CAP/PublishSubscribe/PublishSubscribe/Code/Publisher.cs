namespace PublishSubscribe.Code;

public class TestEvent
{
    public string Name { get; set; } = "事件名";

    public string Message { get; set; } = "信息";
}

public interface IEventHandler
{
    Task ExecuteAsync(TestEvent testEvent);
}

public interface IEventPublisher
{
    Task PublishAsync(TestEvent testEvent);
}

public class EventPublisher : IEventPublisher
{
    private readonly IEnumerable<IEventHandler> _handlers;

    public EventPublisher(IEnumerable<IEventHandler> handlers)
    {
        _handlers = handlers;
    }

    public async Task PublishAsync(TestEvent testEvent)
    {
        foreach(var handler in _handlers)
        {
            try
            {
                await handler.ExecuteAsync(testEvent);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{testEvent.Name}订阅失败，错误信息：{ex.Message}");
            }
        }
    }
}
