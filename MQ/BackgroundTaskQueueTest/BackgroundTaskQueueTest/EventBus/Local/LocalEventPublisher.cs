﻿using BackgroundTaskQueueTest.EventBus.Abstract;
using BackgroundTaskQueueTest.EventBus.Local;

namespace BackgroundTaskQueueTest.EventBus.Default;

public class LocalEventPublisher : IEventPublisher
{
    private readonly IEventStore _eventStore;

    public LocalEventPublisher(IEventStore eventStore)
    {
        _eventStore = eventStore;
    }

    public virtual async Task PublishAsync<TEvent>(TEvent @event)
        where TEvent : class, IEventModel
    {
        await _eventStore.WriteAsync<TEvent>(@event);
    }
}
