using Paramore.Brighter;

public class AuditEvent : Event
{
    public AuditEvent(string eventType) : base(Guid.NewGuid())
    {
        ModifiedAt = DateTime.UtcNow;
        EventType = eventType;
    }

    public string AggregateRootType { get; set; }
    public string AggregateRootId { get; set; }
    public string ParentType { get; set; }
    public string ParentTypeId { get; set; }
    public DateTime ModifiedAt { get; set; }
    public Guid EntityId { get; set; }
    public string EventType { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public IEnumerable<UpdatedAttribute> Changes { get; set; } = new List<UpdatedAttribute>();
}