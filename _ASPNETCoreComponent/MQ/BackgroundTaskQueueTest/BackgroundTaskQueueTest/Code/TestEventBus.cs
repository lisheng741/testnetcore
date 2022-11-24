using BackgroundTaskQueueTest.EventBus.Abstract;
using BackgroundTaskQueueTest.EventBus.Core;

namespace BackgroundTaskQueueTest.Code;

public class TestEventModel : EventModel
{
    public TestEventModel()
    {
    }

    public string Message { get; set; } = "测试消息";
}

public class TestEventHandler : IEventHandler<TestEventModel>
{
    public Task Handle(TestEventModel @event)
    {
        Console.WriteLine("订阅----------------");
        Console.WriteLine(@event.Id);
        Console.WriteLine(@event.UtcNow.ToLocalTime());
        Console.WriteLine(@event.Message);
        Console.WriteLine("订阅 end ----------------");
        return Task.CompletedTask;
    }
}

public class TestErrorEventHandler : IEventHandler<TestEventModel>
{
    public Task Handle(TestEventModel @event)
    {
        //throw new Exception("测试订阅错误");

        Console.WriteLine("订阅2222222----------------");
        Console.WriteLine(@event.Id);
        Console.WriteLine(@event.UtcNow.ToLocalTime());
        Console.WriteLine(@event.Message);
        Console.WriteLine("订阅22222 end ----------------");
        return Task.CompletedTask;
    }
}
