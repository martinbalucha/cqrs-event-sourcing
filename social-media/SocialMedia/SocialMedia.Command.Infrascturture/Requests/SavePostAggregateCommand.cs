using MediatR;
using SocialMedia.CQRS.Core.Domain;

namespace SocialMedia.Command.Infrascturture.Requests;

public record SavePostAggregateCommand(AggregateRoot Aggregate) : IRequest;
