using AuditTrailService.Ports.Json;
using Newtonsoft.Json;
using Paramore.Brighter;

public class MessageMapper<TRequest> : IAmAMessageMapper<TRequest> where TRequest : class, IRequest
{
    public JsonSerializerSettings SerializerSettings { get; set; } = GlobalJsonSerializerSettings.Settings;

    public Message MapToMessage(TRequest request)
    {
        var messageType = request is IEvent ? MessageType.MT_EVENT : MessageType.MT_COMMAND;
        var topic = GetTopic();
        var header = new MessageHeader(messageId: request.Id, topic: topic, messageType: messageType, correlationId: Guid.NewGuid(), partitionKey: request.Id.ToString());
        var body = new MessageBody(JsonConvert.SerializeObject(request, SerializerSettings));
        var message = new Message(header, body);

        var messageHeaders = new Dictionary<string, object>();
        messageHeaders.Add("tenantId", Guid.NewGuid());
        messageHeaders.Add("productInstanceId", Guid.NewGuid());
        messageHeaders.Add("messageSchemaVersion", "1.0.0");

        header.Bag = messageHeaders;

        return message;
    }

    public virtual string GetTopic()
    {
        var type = typeof(TRequest);

        return type.FullName.Replace(".", "-");
    }

    public TRequest MapToRequest(Message message)
    {
        var request = JsonConvert.DeserializeObject<TRequest>(message.Body.Value);

        return request;
    }
}