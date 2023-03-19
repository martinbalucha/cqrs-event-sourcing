using MediatR;
using SocialMedia.CQRS.Core.Messages;

namespace SocialMedia.CQRS.Core.Commands;

public interface ICommand<T> : IMessage, IRequest<T>
{
}
