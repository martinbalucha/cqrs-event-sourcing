using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class MessageUpdatedEvent : IEvent
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Type => nameof(MessageUpdatedEvent);
    public required string Message { get; set; }
}
