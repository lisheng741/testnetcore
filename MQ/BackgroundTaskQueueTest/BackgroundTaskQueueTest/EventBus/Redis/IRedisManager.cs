using BackgroundTaskQueueTest.EventBus.Abstract;
using StackExchange.Redis;

namespace BackgroundTaskQueueTest.EventBus.Redis;

public interface IRedisManager
{
    IConnectionMultiplexer Connection { get; }

    ISubscriber Subscriber { get; }

    void StartSubscribe();

    Task PublishAsync<TEvent>(TEvent @event, CancellationToken token = default)
        where TEvent : class, IEventModel;
}
