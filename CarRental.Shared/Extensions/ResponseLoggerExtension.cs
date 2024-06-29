using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using Serilog.Formatting.Compact;

namespace CarRental.Shared.Extensions;

public static class ResponseLoggerExtension
{
    public static IServiceCollection ConfigureLogger(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
        .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
        .Enrich.FromLogContext()
        .WriteTo.Console(new RenderedCompactJsonFormatter())
        .WriteTo.Logger(lc => lc
            .Filter.ByIncludingOnly(e => e.Level == LogEventLevel.Error)
            .WriteTo.File("Logs\\Error-.log", rollingInterval: RollingInterval.Day))
        .CreateLogger();

        services.AddLogging(loggingBuilder =>
        {
            loggingBuilder.ClearProviders();
            loggingBuilder.AddSerilog(dispose: true);
        });

        return services;
    }
}

