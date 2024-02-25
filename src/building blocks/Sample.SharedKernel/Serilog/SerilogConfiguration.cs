using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Serilog.Events;
using Serilog;
using Serilog.Exceptions;

namespace Sample.SharedKernel.Serilog
{
    public static class SerilogConfiguration
    {
        public static WebApplicationBuilder AddSerilogConfiguration(this WebApplicationBuilder webApplicationBuilder, IConfiguration configuration)
        {
            string applicationName = configuration["ApplicationName"];

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Override("Microsoft.AspNetCore", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .Enrich.WithExceptionDetails()
            .Enrich.WithCorrelationId()
            .Enrich.WithProperty("ApplicationName", $"{applicationName} - {Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")}")
            .WriteTo.Async(wt => wt.Console(outputTemplate: "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj} {Properties:j}{NewLine}{Exception}"))
            .CreateLogger();

            webApplicationBuilder.Host.UseSerilog(Log.Logger);

            return webApplicationBuilder;
        }
    }
}
