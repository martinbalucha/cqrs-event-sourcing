using Microsoft.Extensions.Options;
using MongoDB.Driver;
using SocialMedia.Command.Infrascturture.Config;
using SocialMedia.CQRS.Core.Domain;
using SocialMedia.CQRS.Core.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Command.Infrascturture.Persistence;

public class MongoEventStoreRepository : IEventStoreRepository
{
    private readonly IMongoCollection<EventModel> mongoCollection;

    public MongoEventStoreRepository(IMongoCollection<EventModel> mongoCollection, IOptions<MongoDbConfiguration> configuration)
    {
        this.mongoCollection = mongoCollection ?? throw new ArgumentNullException(nameof(mongoCollection));
    }

    public Task<IEnumerable<EventModel>> FindByAggregateId(Guid aggregateId)
    {
        throw new NotImplementedException();
    }

    public Task SaveAsync(EventModel eventModel)
    {
        throw new NotImplementedException();
    }
}
