using SocialMedia.Command.Domain.Aggregates;
using SocialMedia.CQRS.Core.Domain;
using SocialMedia.CQRS.Core.Events;
using SocialMedia.CQRS.Core.Exceptions;
using SocialMedia.CQRS.Core.Infrastructure;

namespace SocialMedia.Command.Infrascturture.Persistence;

public class EventStore : IEventStore
{
    private const int NewExpectedEventVersion = -1;

    private readonly IEventStoreRepository eventStoreRepository;

    public EventStore(IEventStoreRepository eventStoreRepository)
    {
        this.eventStoreRepository = eventStoreRepository ?? throw new ArgumentNullException(nameof(eventStoreRepository));
    }

    public async Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId)
    {
        var eventStream = await eventStoreRepository.FindByAggregateId(aggregateId);

        if (eventStream is null || !eventStream.Any())
        {
            throw new NotFoundException($"Aggregate with ID '{aggregateId}' does not exist.");
        }

        return eventStream.OrderBy(e => e.Version).Select(e => e.EventData);
    }

    public async Task SaveAsync(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion)
    {
        var eventStream = await eventStoreRepository.FindByAggregateId(aggregateId);

        if (expectedVersion != NewExpectedEventVersion && events.Last().Version != expectedVersion)
        {
            throw new ConcurrencyException();
        }

        int version = expectedVersion;

        foreach (var @event in events)
        {
            version++;
            @event.Version = version;
            var eventType = @event.GetType().Name;

            var eventModel = new EventModel
            {
                TimeStamp = DateTimeOffset.Now,
                AggregateId = aggregateId,
                AggregateType = nameof(PostAggregate),
                Version = version,
                EventType = eventType,
                EventData = @event
            };
        }
    }
}
