using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;
internal class MessageUpdatedEvent : IEvent
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public string Type => nameof(MessageUpdatedEvent);
    public required string Message { get; set; }
}
