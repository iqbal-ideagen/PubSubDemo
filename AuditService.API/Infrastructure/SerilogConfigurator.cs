using Serilog;
using Serilog.Events;

namespace AuditService.API.Infrastructure;

public static class SerilogConfigurator
{
    #region Public Methods

    public static LoggerConfiguration ConfigureLogger()
    {
        //note: see https://github.com/serilog/serilog/wiki/Formatting-Output
        const string logFormat =
            "{Timestamp:o}\t0\t{CorrelationId}\t{Level:u4}\t{Context}\t{Message:lj}{NewLine}{Exception}";

        var loggerConfig = new LoggerConfiguration()
            .Enrich.FromLogContext()
            .MinimumLevel.Verbose()
            .MinimumLevel.Override("System", LogEventLevel.Information)
            .MinimumLevel.Override("Microsoft", LogEventLevel.Debug)
            .MinimumLevel.Override("Microsoft.Hosting.Lifetime", LogEventLevel.Information)
            //note: writes anything >= Error to STDERR
            .WriteTo.Console(standardErrorFromLevel: LogEventLevel.Error,
                restrictedToMinimumLevel: LogEventLevel.Error, outputTemplate: logFormat);

        loggerConfig.WriteTo.Console(restrictedToMinimumLevel: LogEventLevel.Debug, outputTemplate: logFormat);

        return loggerConfig;
    }

    #endregion
}