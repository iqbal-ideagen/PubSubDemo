using AuditService.Domain.Ports.Commands;
using AuditService.Domain.Ports.Json;
using Newtonsoft.Json;
using Paramore.Brighter;

namespace AuditService.Domain.Ports.Mappers;

public class CreateTaskCommandMessageMapper : IAmAMessageMapper<CreateTaskCommand>
{
    public Message MapToMessage(CreateTaskCommand request)
    {
        var header = new MessageHeader(
            messageId: request.Id,
            topic: "task-service.commands",
            messageType: MessageType.MT_COMMAND,
            correlationId: request.ReplyAddress.CorrelationId,
            replyTo: request.ReplyAddress.Topic);

        var messageHeaders = new Dictionary<string, object>();
        messageHeaders.Add("tenantId", Guid.NewGuid().ToString());
        messageHeaders.Add("productInstanceId", Guid.NewGuid().ToString());
        messageHeaders.Add("messageSchemaVersion", "1.0.0");

        header.Bag = messageHeaders;

        var body = new MessageBody(JsonConvert.SerializeObject(request, GlobalJsonSerializerSettings.Settings));
        var message = new Message(header, body);
        return message;
    }

    public CreateTaskCommand MapToRequest(Message message)
    {
        throw new NotImplementedException();
    }
}