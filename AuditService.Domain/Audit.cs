namespace AuditService.Domain;

public class Audit
{
    public Audit(string title, string code, string summary)
    {
        Title = title;
        Code = code;
        Summary = summary;
        StartDate = DateTime.UtcNow;
        Status = "Planned";
    }

    public Guid Id { get; set; }
    public string Title { get; set; }
    public string Code { get; set; }
    public string Summary { get; set; }
    public string Status { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }

    public void CompleteAudit()
    {
        Status = "Completed";
    }
}