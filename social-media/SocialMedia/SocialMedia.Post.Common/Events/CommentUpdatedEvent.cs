using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class CommentUpdatedEvent : IEvent
{
    public Guid Id { get; init; }
    public int Version { get; init; }
    public string Type => nameof(CommentUpdatedEvent);
    public required Guid CommentId { get; init; }
    public required string CommentText { get; set; }
    public required string Username { get; set; }
    public DateTimeOffset EditDate { get; init; }
}
