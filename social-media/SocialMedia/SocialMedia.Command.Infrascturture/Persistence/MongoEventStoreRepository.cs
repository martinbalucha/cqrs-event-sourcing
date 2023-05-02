using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialMedia.Command.Infrascturture.Config;
using SocialMedia.CQRS.Core.Domain;
using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Command.Infrascturture.Persistence;

public class MongoEventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> eventStoreCollection;

    public MongoEventStoreRepository(IMongoCollection<EventModel> mongoCollection, IOptions<MongoDbConfiguration> configuration)
    {
        this.eventStoreCollection = mongoCollection ?? throw new ArgumentNullException(nameof(mongoCollection));
    }

    public async Task<IEnumerable<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        return (await eventStoreCollection.FindAsync(e => e.AggregateId == aggregateId).ConfigureAwait(false)).ToEnumerable();
    }

    public async Task SaveAsync(EventModel eventModel)
    {
        await eventStoreCollection.InsertOneAsync(eventModel).ConfigureAwait(false);
    }
}
