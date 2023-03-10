namespace AuditService.Domain
{
    public interface IAuditRepository
    {
        Audit Add(Audit audit);
        Audit Get(Guid id);

        List<Audit> GetAll();
    }
}