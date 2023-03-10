using Newtonsoft.Json;

namespace AuditService.Domain.Ports.Messages;

public class UpdatedAttribute
{
    [JsonProperty("op")]
    public string Operation { get; set; }

    [JsonProperty("path")]
    public string Path { get; set; }

    [JsonProperty("value")]
    public string NewAttributeValue { get; set; }

    [JsonProperty("old_value")]
    public string OldAttributeValue { get; set; }
}