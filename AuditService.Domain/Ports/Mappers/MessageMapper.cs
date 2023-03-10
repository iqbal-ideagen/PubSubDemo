using AuditService.Domain.Ports.Json;
using AuditService.Domain.Ports.TopicResolver;
using Newtonsoft.Json;
using Paramore.Brighter;

namespace AuditService.Domain.Ports.Mappers;

public class MessageMapper<TRequest> : IAmAMessageMapper<TRequest> where TRequest : class, IRequest
{
    private readonly ITopicResolver _topicResolver;

    public MessageMapper(ITopicResolver topicResolver)
    {
        _topicResolver = topicResolver;
    }

    public JsonSerializerSettings SerializerSettings { get; set; } = GlobalJsonSerializerSettings.Settings;

    public Message MapToMessage(TRequest request)
    {
        var messageType = request is IEvent ? MessageType.MT_EVENT : MessageType.MT_COMMAND;

        var topic = _topicResolver.GetTopicName<TRequest>();

        var header = new MessageHeader(messageId: request.Id, topic: topic, messageType: messageType, correlationId: Guid.NewGuid(), partitionKey: GetKey(request));

        var messageHeaders = new Dictionary<string, object>();
        messageHeaders.Add("tenantId", Guid.NewGuid());
        messageHeaders.Add("productInstanceId", Guid.NewGuid());
        messageHeaders.Add("messageSchemaVersion", "1.0.0");

        header.Bag = messageHeaders;

        var body = new MessageBody(JsonConvert.SerializeObject(request, SerializerSettings));
        var message = new Message(header, body);

        return message;
    }

    public TRequest MapToRequest(Message message)
    {
        var request = JsonConvert.DeserializeObject<TRequest>(message.Body.Value);

        return request;
    }

    public virtual string GetKey(TRequest request)
    {
        return request.Id.ToString();
    }
}