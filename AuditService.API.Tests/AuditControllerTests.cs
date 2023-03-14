using AuditService.API.Controllers;
using AuditService.API.Models;
using AuditService.Domain;
using AuditService.Infrastructure;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Paramore.Brighter;
using Xunit;

namespace AuditService.API.Tests
{
    public class AuditControllerTests
    {
        [Fact]
        public async Task Create_WithValidData_ReturnsResult()
        {
            // Arrange
            var commandProcessorMock = new Mock<IAmACommandProcessor>();

            var controller = new AuditController(commandProcessorMock.Object, new InMemoryAuditRepository());

            var auditDto = new AuditDto
            {
                Code = "code test",
                Summary = "summary test",
                Title = "title test"
            };

            // Act
            var response = await controller.Create(auditDto);

            // Assert
            response.Should().BeOfType<OkObjectResult>();
            var okObject = response.As<OkObjectResult>();
            okObject.Value.Should().BeOfType<Audit>();

            var audit = okObject.Value.As<Audit>();
            audit.Title.Should().Be(auditDto.Title);
            audit.Code.Should().Be(auditDto.Code);
            audit.Summary.Should().Be(auditDto.Summary);

        }

    }
}