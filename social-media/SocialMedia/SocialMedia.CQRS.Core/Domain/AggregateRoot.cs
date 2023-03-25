using SocialMedia.CQRS.Core.Events;

namespace SocialMedia.CQRS.Core.Domain;

public abstract class AggregateRoot
{
    private const string ApplyMethodName = "Apply";
    private readonly List<IEvent> changes = new();

    public Guid Id { get; set; }
    public int Version { get; set; } = -1;

    public IEnumerable<IEvent> GetUncommittedChanges()
    {
        return changes;
    }

    public void MarkChangesAsCommitted()
    {
        changes.Clear();
    }

    public void ReplayEvents(IEnumerable<IEvent> events)
    {
        foreach (var @event in events)
        {
            ApplyChange(@event);
        }
    }

    protected void RaiseEvent(IEvent @event)
    {
        ApplyChange(@event);
    }

    private void ApplyChange(IEvent @event)
    {
        var method = GetType().GetMethod(ApplyMethodName, new Type[] { @event.GetType() });

        ArgumentNullException.ThrowIfNull(method, $"The apply message was not found in the aggregate for {@event.GetType().Name}");

        method.Invoke(this, new object[] { @event });

        if (!changes.Contains(@event))
        {
            changes.Add(@event);
        }
    }
}
