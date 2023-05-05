using MediatR;
using SocialMedia.Command.Infrascturture.Requests;
using SocialMedia.CQRS.Core.Infrastructure;

namespace SocialMedia.Command.Infrascturture.Handlers;

public class SavePostAggregateHandler : IRequestHandler<SavePostAggregateCommand>
{
    private readonly IEventStore eventStore;

    public SavePostAggregateHandler(IEventStore eventStore)
    {
        this.eventStore = eventStore ?? throw new ArgumentNullException(nameof(eventStore));
    }

    public async Task Handle(SavePostAggregateCommand request, CancellationToken cancellationToken)
    {
        await eventStore.SaveAsync(request.Aggregate.Id, request.Aggregate.GetUncommittedChanges(), request.Aggregate.Version);
        request.Aggregate.MarkChangesAsCommitted();
    }
}
