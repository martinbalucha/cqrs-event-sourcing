using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.CQRS.Core.Infrastructure;

public interface IEventStore
{
    Task SaveAsync(Guid aggregateId, IEnumerable<IEvent> events, int expectedVersion);
    Task<IEnumerable<IEvent>> GetEventsAsync(Guid aggregateId);
}
