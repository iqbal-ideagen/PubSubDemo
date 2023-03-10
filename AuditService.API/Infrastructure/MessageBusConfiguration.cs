using Paramore.Brighter.MessagingGateway.Kafka;

namespace AuditService.API.Infrastructure;

public class MessageBusConfiguration
{
    public KafkaConfiguration KafkaConfiguration { get; set; }
    public PublicationTopics PublicationTopics { get; set; }
}

public class PublicationTopics
{
    public string AuditServiceEvents { get; set; }
    public string TaskServiceCommands { get; set; }
}

public class KafkaConfiguration
{
    public string[] BootStrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public SaslMechanism? SaslMechanisms { get; set; } = null;
    public SecurityProtocol? SecurityProtocol { get; set; } = null;
}