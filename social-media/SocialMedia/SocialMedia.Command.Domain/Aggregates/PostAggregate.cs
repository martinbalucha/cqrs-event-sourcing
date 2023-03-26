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

    public void AddComment(UserComment userComment)
    {
        if (!Active)
        {
            throw new InvalidOperationException("An inactive post cannot be commented.");
        }
        if (string.IsNullOrWhiteSpace(userComment.Comment))
        {
            throw new InvalidOperationException($"The comment cannot be null or white space.");
        }

        var commmentAddedEvent = new CommentAddedEvent
        {
            Id = Id,
            CommentId = Guid.NewGuid(),
            Comment = userComment.Comment,
            Username = userComment.Username,
            CommentDate = DateTimeOffset.Now,
        };

        RaiseEvent(commmentAddedEvent);
    }

    public void Apply(CommentAddedEvent commentAddedEvent)
    {
        Id = commentAddedEvent.Id;
        comments.Add(commentAddedEvent.Id, new UserComment(commentAddedEvent.Username, commentAddedEvent.Comment));
    }

    public void EditComment(Guid commentId, UserComment userComment)
    {
        if (!Active)
        {
            throw new InvalidOperationException($"An inactive post cannot be edited.");
        }

        if (!comments[commentId].Username.Equals(userComment.Username, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException("A comment written by a different user cannot be edited.");
        }

        var commentUpdatedEvent = new CommentUpdatedEvent
        {
            Id = Id,
            CommentId = commentId,
            CommentText = userComment.Comment,
            Username = userComment.Username,
            EditDate = DateTimeOffset.Now
        };

        RaiseEvent(commentUpdatedEvent);
    }

    public void Apply(CommentUpdatedEvent commentUpdatedEvent)
    {
        Id = commentUpdatedEvent.Id;
        comments[commentUpdatedEvent.Id] = new UserComment(commentUpdatedEvent.Username, commentUpdatedEvent.CommentText);
    }

    public void RemoveComment(Guid commentId, string username)
    {
        if (!Active)
        {
            throw new InvalidOperationException($"An inactive post cannot be removed.");
        }
        if (!comments[commentId].Username.Equals(username, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException("A comment written by a different user cannot be removed.");
        }

        var commentRemovedEvent = new CommentRemovedEvent
        {
            Id = Id,
            CommentId = commentId
        };

        RaiseEvent(commentRemovedEvent);
    }

    public void Apply(CommentRemovedEvent commentRemovedEvent)
    {
        Id = commentRemovedEvent.Id;
        comments.Remove(commentRemovedEvent.Id);
    }

    public void DeletePost(string username)
    {
        if (!Active)
        {
            throw new InvalidOperationException($"The post has already been removed.");
        }

        if (!Author.Equals(username, StringComparison.InvariantCultureIgnoreCase))
        {
            throw new InvalidOperationException("A post written by a different user cannot be removed.");
        }

        var postRemovedEvent = new PostRemovedEvent
        {
            Id = Id
        };

        RaiseEvent(postRemovedEvent);
    }

    public void Apply(PostRemovedEvent posrtRemovedEvent)
    {
        Id = posrtRemovedEvent.Id;
        Active = false;
    }
}