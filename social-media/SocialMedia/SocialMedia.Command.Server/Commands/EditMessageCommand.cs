using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class EditMessageCommand : ICommand
{
    public Guid Id { get; init; }
    public required string Message { get; set; }
}
