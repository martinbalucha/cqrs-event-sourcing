using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class PostRemovedEvent : IEvent
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public string Type => nameof(PostRemovedEvent);
}
