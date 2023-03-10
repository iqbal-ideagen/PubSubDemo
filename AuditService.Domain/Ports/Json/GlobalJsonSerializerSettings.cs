using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;

namespace AuditService.Domain.Ports.Json;

public static class GlobalJsonSerializerSettings
{
    public static JsonSerializerSettings Settings { get; }

    static GlobalJsonSerializerSettings()
    {
        var settings = new JsonSerializerSettings();

        settings.Converters.Add(new StringEnumConverter());
        settings.ContractResolver = new CamelCasePropertyNamesContractResolver();
        settings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
        settings.DateTimeZoneHandling = DateTimeZoneHandling.Utc;
        settings.NullValueHandling = NullValueHandling.Ignore;

        Settings = settings;
    }
}