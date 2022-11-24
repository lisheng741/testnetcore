namespace PublishSubscribe.Code;

public class TestEvent
{
    public string Name { get; set; } = "事件名";

    public string Message { get; set; } = "信息";
}

public interface IPublisher
{
    Task PublishAsync(TestEvent testEvent);
}

public class Publisher : IPublisher
{
    private readonly IEnumerable<ISubscriber> _handlers;

    public Publisher(IEnumerable<ISubscriber> handlers)
    {
        _handlers = handlers;
    }

    public async Task PublishAsync(TestEvent testEvent)
    {
        foreach(var handler in _handlers)
        {
            try
            {
                if(handler.EventName == testEvent.Name)
                {
                    await handler.ExecuteAsync(testEvent);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine($"{testEvent.Name}订阅失败，错误信息：{ex.Message}");
            }
        }
    }
}
