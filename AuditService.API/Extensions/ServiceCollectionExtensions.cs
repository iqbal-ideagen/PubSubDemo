using AuditService.API.Controllers;
using AuditService.API.Infrastructure;
using AuditService.Domain;
using AuditService.Domain.Ports.Commands;
using AuditService.Domain.Ports.Events;
using AuditService.Domain.Ports.TopicResolver;
using AuditService.Infrastructure;
using Confluent.Kafka;
using Paramore.Brighter;
using Paramore.Brighter.Extensions.DependencyInjection;
using Paramore.Brighter.Logging;
using Paramore.Brighter.MessagingGateway.Kafka;

namespace AuditService.API.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddAuditComponents(this IServiceCollection services)
    {
        services.AddSingleton<IAuditRepository>(new InMemoryAuditRepository());

        return services;
    }

    public static IServiceCollection AddEventBus(this IServiceCollection services, IConfiguration configuration)
    {
        var logFactory = services.BuildServiceProvider().GetService<ILoggerFactory>();

        ApplicationLogging.LoggerFactory = logFactory;

        var messageBusConfig = new MessageBusConfiguration();
        configuration.GetSection("MessageBusConfiguration").Bind(messageBusConfig);

        services.AddSingleton<ITopicResolver>(new TopicResolver(r =>
        {
            r.Add(typeof(AuditEvent), messageBusConfig.PublicationTopics.AuditServiceEvents);
            r.Add(typeof(CreateTaskCommand), messageBusConfig.PublicationTopics.TaskServiceCommands);
        }));

        var kafkaConfiguration = new KafkaMessagingGatewayConfiguration
        {
            Name = "kafka.event.bus",
            BootStrapServers = messageBusConfig.KafkaConfiguration.BootStrapServers,
            SaslUsername = messageBusConfig.KafkaConfiguration.SaslUsername,
            SaslPassword = messageBusConfig.KafkaConfiguration.SaslPassword,
            SaslMechanisms = messageBusConfig.KafkaConfiguration.SaslMechanisms,
            SecurityProtocol = messageBusConfig.KafkaConfiguration.SecurityProtocol
        };

        var consumerFactory = new KafkaMessageConsumerFactory(kafkaConfiguration);

        var replySubscriptions = new KafkaSubscription[]
        {
            new KafkaSubscription<CreateTaskReply>(
                new SubscriptionName("create.task.reply.subscription"),
                channelName: new ChannelName("create.task.reply.channel"),
                routingKey: new RoutingKey("create.task.reply"),
                groupId: "create.task.reply.group",
                timeoutInMilliseconds: 100,
                offsetDefault: AutoOffsetReset.Latest,
                commitBatchSize:5)
        };

        services.AddBrighter(options =>
            {
                options.ChannelFactory = new ChannelFactory(consumerFactory);
            })
            .UseInMemoryOutbox()
            .UseExternalBus(
                new KafkaProducerRegistryFactory(kafkaConfiguration,
                        new KafkaPublication[]
                        {
                            new()
                            {
                                Topic = new RoutingKey(messageBusConfig.PublicationTopics.AuditServiceEvents),
                                MessageSendMaxRetries = 3,
                                MessageTimeoutMs = 1000,
                                MaxInFlightRequestsPerConnection = 1
                            }
                        })
                    .Create())
            .AutoFromAssemblies(typeof(Audit).Assembly);

        return services;
    }
}