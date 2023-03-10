using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Paramore.Brighter;
using Paramore.Brighter.MessagingGateway.Kafka;
using Paramore.Brighter.ServiceActivator.Extensions.DependencyInjection;
using Paramore.Brighter.ServiceActivator.Extensions.Hosting;

internal static class Program
{
    public static async Task Main(string[] args)
    {
        string environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

        var builder = new ConfigurationBuilder()
            .SetBasePath(Path.Combine(AppContext.BaseDirectory))
            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
            .AddJsonFile($"appsettings.{environment}.json", optional: true)
            .AddEnvironmentVariables();

        var configuration = builder.Build();

        var messageBusConfiguration = new MessageBusConfiguration();

        configuration.GetSection("MessageBusConfiguration").Bind(messageBusConfiguration);

        var host = new HostBuilder()
            .ConfigureServices((hostContext, services) =>
            {
                var subscriptions = new KafkaSubscription[]
                {
                        new KafkaSubscription<AuditEvent>(
                            new SubscriptionName("audit-service.subscription"),
                            channelName: new ChannelName("audit-service.channel"),
                            routingKey: new RoutingKey("audit-service.events"),
                            groupId: "audit-trail-service.group",
                            timeoutInMilliseconds: 100,
                            offsetDefault: AutoOffsetReset.Earliest,
                            commitBatchSize:5),
                };

                var consumerFactory = new KafkaMessageConsumerFactory(
                    new KafkaMessagingGatewayConfiguration
                    {
                        Name = "kafka.event.bus",
                        BootStrapServers = new[] { "localhost:9093" }

                    }
               );

                services.AddServiceActivator(options =>
                {
                    options.Subscriptions = subscriptions;
                    options.ChannelFactory = new ChannelFactory(consumerFactory);
                }).AutoFromAssemblies();

                services.AddHostedService<ServiceActivatorHostedService>();
            })
            .UseConsoleLifetime()
            .Build();

        await host.RunAsync();
    }
}