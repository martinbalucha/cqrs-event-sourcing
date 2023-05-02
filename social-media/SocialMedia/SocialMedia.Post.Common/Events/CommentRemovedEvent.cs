using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class CommentRemovedEvent : IEvent
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Type => nameof(CommentRemovedEvent);
    public required Guid CommentId { get; init; }
}
