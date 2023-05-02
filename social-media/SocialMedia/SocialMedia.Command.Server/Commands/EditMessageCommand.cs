﻿using MediatR;
using SocialMedia.CQRS.Core.Commands;

namespace SocialMedia.Command.Server.Commands;

public class EditMessageCommand : ICommand<Unit>
{
    public Guid Id { get; set; }
    public required string Message { get; set; }
}
