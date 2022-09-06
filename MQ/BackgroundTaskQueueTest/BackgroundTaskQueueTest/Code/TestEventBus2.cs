using BackgroundTaskQueueTest.EventBus.Abstract;
using BackgroundTaskQueueTest.EventBus.Core;

namespace BackgroundTaskQueueTest.Code;

public class Test2EventModel : EventModel
{
    public Test2EventModel()
    {
    }

    public string Message { get; set; } = "第二个测试消息";
}

public class Test2EventHandler : IEventHandler<Test2EventModel>
{
    public Task Handle(Test2EventModel @event)
    {
        Console.WriteLine("订阅----------------");
        Console.WriteLine(@event.Id);
        Console.WriteLine(@event.UtcNow.ToLocalTime());
        Console.WriteLine(@event.Message);
        Console.WriteLine("订阅 end ----------------");
        return Task.CompletedTask;
    }
}

public class Test22EventHandler : IEventHandler<Test2EventModel>
{
    public Task Handle(Test2EventModel @event)
    {
        Console.WriteLine("订阅22222----------------");
        Console.WriteLine(@event.Id);
        Console.WriteLine(@event.UtcNow.ToLocalTime());
        Console.WriteLine(@event.Message);
        Console.WriteLine("订阅22222222 end ----------------");
        return Task.CompletedTask;
    }
}
