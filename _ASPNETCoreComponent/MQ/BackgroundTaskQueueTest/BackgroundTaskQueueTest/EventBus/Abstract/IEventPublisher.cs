namespace BackgroundTaskQueueTest.EventBus.Abstract;

public interface IEventPublisher
{
    Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel;
}
