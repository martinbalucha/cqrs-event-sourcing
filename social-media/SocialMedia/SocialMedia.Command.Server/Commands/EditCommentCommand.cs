﻿using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class EditCommentCommand : ICommand
{
    public Guid Id { get; init ; }
    public required string Comment { get; set; }
    public required string Username { get; init; }
}
