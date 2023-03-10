using Paramore.Brighter;

namespace AuditService.API.Extensions;

public class CreateTaskReply : Reply
{
    public CreateTaskReply() : base(new ReplyAddress())
    {

    }

    public CreateTaskReply(string topic, Guid correlationId) : base(new ReplyAddress(topic, correlationId))
    {

    }
    public Guid AssociatedEntityId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime DueDate { get; set; }
    public Guid Owner { get; set; }
    public string Status { get; set; }
}