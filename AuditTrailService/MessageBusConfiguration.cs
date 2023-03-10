using Paramore.Brighter.MessagingGateway.Kafka;

public class MessageBusConfiguration
{
    public KafkaConfiguration KafkaConfiguration { get; set; }
    public SubscriptionTopics SubscriptionTopics { get; set; }
}

public class SubscriptionTopics
{
    public string TaskServiceEvents { get; set; }
    public string AuditServiceEvents { get; set; }
}

public class KafkaConfiguration
{
    public string[] BootStrapServers { get; set; }
    public string SaslUsername { get; set; }
    public string SaslPassword { get; set; }
    public SaslMechanism? SaslMechanisms { get; set; } = null;
    public SecurityProtocol? SecurityProtocol { get; set; } = null;
}