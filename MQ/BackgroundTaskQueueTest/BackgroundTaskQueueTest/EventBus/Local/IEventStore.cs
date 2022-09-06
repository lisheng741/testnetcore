using BackgroundTaskQueueTest.EventBus.Abstract;

namespace BackgroundTaskQueueTest.EventBus.Local;

public interface IEventStore
{
    ValueTask WriteAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel;

    ValueTask<object> ReadAsync(CancellationToken cancellationToken);
}
