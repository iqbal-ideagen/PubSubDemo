using AuditService.Domain.Ports.Events;
using AuditService.Domain.Ports.TopicResolver;

namespace AuditService.Domain.Ports.Mappers;

public class AuditCreatedMessageMapper : MessageMapper<AuditEvent>
{
    public AuditCreatedMessageMapper(ITopicResolver topicResolver) : base(topicResolver)
    {

    }

    public override string GetKey(AuditEvent request)
    {
        return request.EntityId.ToString();
    }
}