using AuditService.Domain.Ports.Events;

namespace AuditService.Domain;

public static class AuditExtensions
{
    public static AuditEvent ToAuditCreatedEvent(this Audit audit)
    {
        var @event = new AuditEvent("audit.create")
        {
            EntityId = audit.Id,
            Title = audit.Title,
            Code = audit.Code,
            Summary = audit.Summary,
            Status = audit.Status,
            StartDate = audit.StartDate
        };

        return @event;
    }
}