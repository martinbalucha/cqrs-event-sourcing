using MediatR;
using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class LikePostCommand : ICommand<Unit>
{
    public Guid Id { get; init; }
}
