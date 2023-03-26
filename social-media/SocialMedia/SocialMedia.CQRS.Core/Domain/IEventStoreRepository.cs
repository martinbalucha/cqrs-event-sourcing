using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.CQRS.Core.Domain;

public interface IEventStoreRepository
{
    Task SaveAsync(EventModel eventModel);
    Task<IEnumerable<EventModel>> FindByAggregateId(Guid aggregateId);
}
