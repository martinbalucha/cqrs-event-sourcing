using MediatR;
using SocialMedia.Command.Domain.Aggregates;
using SocialMedia.Command.Infrascturture.Requests;
using SocialMedia.CQRS.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SocialMedia.Command.Infrascturture.Handlers;

public class GetPostAggregateByIdHandler : IRequestHandler<GetPostByIdQuery, PostAggregate>
{
    private readonly IEventStore eventStore;

    public GetPostAggregateByIdHandler(IEventStore eventStore)
    {
        this.eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
    }

    public async Task<PostAggregate> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
    {
        var aggregate = new PostAggregate();
        var events = await eventStore.GetEventsAsync(request.AggregateId);

        if (events is null || !events.Any())
        {
            return aggregate;
        }

        aggregate.ReplayEvents(events);
        aggregate.Version = events.Max(e => e.Version);

        return aggregate;
    }
}
