using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class NewPostCommand : ICommand
{
    public Guid Id { get; init; }
    public required string Author { get; set; }
    public required string Message { get; set; }
}
