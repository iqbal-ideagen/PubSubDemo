using AuditTrailService.Ports.EventHandlers;
using FluentAssertions;
using Xunit;

namespace AuditTrailService.Tests
{
    public class AuditCreatedHandlerTests
    {
        [Fact]
        public void Handle_WithAuditEvent_ReturnsResult()
        {
            // Arrange
            var auditEvent = new AuditEvent("create")
            {
                Title = "Audit title",
                Code = "Audit code",
                EntityId = Guid.NewGuid()
            };

            var handler = new AuditCreatedHandler();

            // Act
            var result = handler.Handle(auditEvent);

            // Assert
            result.Should().NotBeNull();
            result.Title.Should().Be(auditEvent.Title);
            result.Code.Should().Be(auditEvent.Code);
            result.EntityId.Should().Be(auditEvent.EntityId);
        }
    }
}