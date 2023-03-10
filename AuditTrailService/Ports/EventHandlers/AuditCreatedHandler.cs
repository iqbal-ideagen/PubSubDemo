using AuditTrailService.Ports.Json;
using Newtonsoft.Json;
using Paramore.Brighter;

namespace AuditTrailService.Ports.EventHandlers
{
    public class AuditCreatedHandler : RequestHandler<AuditEvent>
    {
        public override AuditEvent Handle(AuditEvent @event)
        {
            Console.WriteLine("Audit Event Received:");
            Console.WriteLine("----------------------------------");
            Console.WriteLine(JsonConvert.SerializeObject(@event, GlobalJsonSerializerSettings.Settings));
            Console.WriteLine("----------------------------------");
            Console.WriteLine("Event logged");

            return base.Handle(@event);
        }
    }
}

