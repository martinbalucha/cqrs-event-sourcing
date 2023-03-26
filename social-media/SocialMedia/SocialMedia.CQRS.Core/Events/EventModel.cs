using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace SocialMedia.CQRS.Core.Events;

public class EventModel
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public required string Id { get; init; }
    public DateTimeOffset TimeStamp { get; set; }
    public Guid AggregateId { get; set; }
    public string AggregateType { get; set; }
    public int Version { get; set; }
    public string EventType { get; set; }
    public required IEvent EventData { get; set; }
}
