using MediatR;
using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class RemoveCommentCommand : ICommand<Unit>
{
    public Guid Id { get; set; }

    public Guid CommentId { get; init; }

    public required string Username { get; init; }
}
