using AuditService.Domain;

namespace AuditService.Infrastructure
{
    public class InMemoryAuditRepository : IAuditRepository
    {
        private static List<Audit> _audits = new List<Audit>();

        public Audit Add(Audit audit)
        {
            audit.Id = Guid.NewGuid();

            _audits.Add(audit);

            return audit;
        }

        public Audit Get(Guid id)
        {
            return _audits.FirstOrDefault(a => a.Id == id);
        }

        public List<Audit> GetAll()
        {
            return _audits;
        }
    }
}