using MediatR;
using SocialMedia.Command.Domain.Aggregates;

namespace SocialMedia.Command.Infrascturture.Requests;

public record GetPostByIdQuery(Guid AggregateId) : IRequest<PostAggregate>;