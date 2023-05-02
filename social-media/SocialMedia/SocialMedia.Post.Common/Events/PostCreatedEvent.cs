using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class PostCreatedEvent : IEvent
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Type => nameof(PostCreatedEvent);
    public required string Message { get; set; }
    public required string Author { get; init; }
    public required DateTimeOffset DatePosted { get; init; }
}
