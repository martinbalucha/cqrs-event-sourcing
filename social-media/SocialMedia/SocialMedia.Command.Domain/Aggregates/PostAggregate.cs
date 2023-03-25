using SocialMedia.CQRS.Core.Domain;
using SocialMedia.Post.Common.Events;

namespace SocialMedia.Command.Domain.Aggregates;

public class PostAggregate : AggregateRoot
{
    private readonly Dictionary<Guid, UserComment> comments = new();

    private bool Active { get; set; }
    private string Author { get; set; }

    public PostAggregate()
    {
    }

    public PostAggregate(Guid postId, string author, string message)
    {
        var postCreatedEvent = new PostCreatedEvent
        {
            Id = postId,
            DatePosted = DateTimeOffset.Now,
            Message = message,
            Author = author,
        };

        RaiseEvent(postCreatedEvent);
    }

    public void Apply(PostCreatedEvent postCreatedEvent)
    {
        Id = postCreatedEvent.Id;
        Active = true;
        Author = postCreatedEvent.Author;
    }

    public void EditMessage(string message)
    {
        if (!Active)
        {
            throw new InvalidOperationException("An inactive post cannot be edited.");
        }

        if (string.IsNullOrWhiteSpace(message))
        {
            throw new InvalidOperationException($"The message cannot be null or white space.");
        }

        var messageUpdatedEvent = new MessageUpdatedEvent
        {
            Id = Id,
            Message = message
        };

        RaiseEvent(messageUpdatedEvent);
    }

    public void Apply(MessageUpdatedEvent messageUpdatedEvent)
    {
        Id = messageUpdatedEvent.Id;
    }

    public void LikePost()
    {
        if (!Active)
        {
            throw new InvalidOperationException("An inactive post cannot be liked.");
        }

        var postLikedEvent = new PostLikedEvent
        { 
            Id = Id 
        };

        RaiseEvent(postLikedEvent);
    }

    public void Apply(PostLikedEvent postLikedEvent)
    {
        Id = postLikedEvent.Id;
    }

    public void AddComment(string comment, string username)
    {
        if (!Active)
        {
            throw new InvalidOperationException("An inactive post cannot be commented.");
        }
        if (string.IsNullOrWhiteSpace(comment))
        {
            throw new InvalidOperationException($"The comment cannot be null or white space.");
        }

        var commmentAddedEvent = new CommentAddedEvent
        { 
            Id = Id,
            CommentId = Guid.NewGuid(),
            Comment = comment,
            Username = username,
            CommentDate = DateTimeOffset.Now,
        };

        RaiseEvent(commmentAddedEvent);
    }

    public void Apply(CommentAddedEvent commentAddedEvent)
    {
        Id = commentAddedEvent.Id;
        comments.Add(commentAddedEvent.Id, new UserComment(commentAddedEvent.Username, commentAddedEvent.Comment));
    }
}
