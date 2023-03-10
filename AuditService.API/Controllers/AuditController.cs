using AuditService.API.Models;
using AuditService.Domain;
using AuditService.Domain.Ports.Commands;
using Microsoft.AspNetCore.Mvc;
using Paramore.Brighter;

namespace AuditService.API.Controllers
{
    [Route("audits")]
    public class AuditController : Controller
    {
        private readonly IAmACommandProcessor _commandProcessor;
        private readonly IAuditRepository _auditRepository;

        public AuditController(IAmACommandProcessor commandProcessor, IAuditRepository auditRepository)
        {
            _commandProcessor = commandProcessor;
            _auditRepository = auditRepository;
        }

        [HttpPost]
        public async Task<IActionResult> Create(AuditDto auditDto)
        {
            var audit = new Audit(auditDto.Title, auditDto.Code, auditDto.Summary);

            audit = _auditRepository.Add(audit);

            var auditCreatedEvent = audit.ToAuditCreatedEvent();

            var command = new CreateTaskCommand
            {
                Title = auditDto.Title,
                Description = auditDto.Summary,
                EntityId = audit.Id,
                Owner = Guid.NewGuid()
            };

            await _commandProcessor.PostAsync(auditCreatedEvent);

            await _commandProcessor.PostAsync(command);

            return Ok(audit);
        }
    }
}
