using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class PostRemovedEvent : IEvent
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Type => nameof(PostRemovedEvent);
}
