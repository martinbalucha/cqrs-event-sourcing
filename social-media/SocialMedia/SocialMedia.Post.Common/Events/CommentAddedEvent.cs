using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.Post.Common.Events;

public class CommentAddedEvent : IEvent
{
    public Guid Id { get; set; }
    public int Version { get; set; }
    public string Type => nameof(CommentAddedEvent);
    public required Guid CommentId { get; init; }
    public required string Comment { get; set; }
    public required string Username { get; init; }
    public DateTimeOffset CommentDate { get; init; }
}
