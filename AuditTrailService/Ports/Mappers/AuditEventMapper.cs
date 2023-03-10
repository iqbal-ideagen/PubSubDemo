public class AuditEventMapper : MessageMapper<AuditEvent>
{
    public override string GetTopic()
    {
        return "audit-service.events";
    }
}