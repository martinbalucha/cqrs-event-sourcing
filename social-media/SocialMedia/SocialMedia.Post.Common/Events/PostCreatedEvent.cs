using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class PostCreatedEvent : IEvent
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public string Type => nameof(PostCreatedEvent);
    public required string Message { get; set; }
    public required DateTimeOffset DatePosted { get; init; }
}
