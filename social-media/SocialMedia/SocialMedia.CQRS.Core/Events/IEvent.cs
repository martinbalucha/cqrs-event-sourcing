﻿using SocialMedia.CQRS.Core.Messages;

namespace SocialMedia.CQRS.Core.Events;

public interface IEvent : IMessage
{
    int Version { get; init; }
    string Type { get; }
}
