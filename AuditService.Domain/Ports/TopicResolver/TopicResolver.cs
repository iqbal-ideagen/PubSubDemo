using AuditService.Domain.Ports.TopicResolver;

namespace AuditService.API.Extensions;

public class TopicResolver : ITopicResolver
{
    private IDictionary<Type, string> _topics;

    public TopicResolver(Action<IDictionary<Type, string>> registerTopic)
    {
        _topics = new Dictionary<Type, string>();

        registerTopic?.Invoke(_topics);
    }

    public string GetTopicName<T>() where T : class
    {
        if (!_topics.TryGetValue(typeof(T), out var topic))
        {
            throw new ArgumentException($"Topic not found for the request type {typeof(T).Name}");
        }

        return topic;
    }
}