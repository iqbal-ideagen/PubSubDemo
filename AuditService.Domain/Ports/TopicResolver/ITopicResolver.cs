namespace AuditService.Domain.Ports.TopicResolver;

public interface ITopicResolver
{
    string GetTopicName<T>() where T : class;
}