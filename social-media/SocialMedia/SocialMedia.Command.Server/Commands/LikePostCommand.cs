using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class LikePostCommand : ICommand
{
    public Guid Id { get; init; }
}
