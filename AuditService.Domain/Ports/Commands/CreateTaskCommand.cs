using Paramore.Brighter;

namespace AuditService.Domain.Ports.Commands;

public class CreateTaskCommand : Request
{
    public CreateTaskCommand() : base(new ReplyAddress("create.task.reply", Guid.NewGuid()))
    {
        CommandType = "task.create";
    }

    public string Title { get; set; }
    public string Description { get; set; }
    public Guid Owner { get; set; }
    public Guid EntityId { get; set; }
    public string CommandType { get; set; }
}