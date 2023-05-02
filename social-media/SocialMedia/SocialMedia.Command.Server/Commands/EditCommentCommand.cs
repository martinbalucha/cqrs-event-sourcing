using MediatR;
using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class EditCommentCommand : ICommand<Unit>
{
    public Guid Id { get; set ; }
    public required string Comment { get; set; }
    public required string Username { get; init; }
}
