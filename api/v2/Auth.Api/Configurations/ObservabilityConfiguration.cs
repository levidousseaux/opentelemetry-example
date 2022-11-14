using OpenTelemetry.Metrics;
using OpenTelemetry.Resources;
using OpenTelemetry.Trace;
using Serilog;
using Serilog.Sinks.Grafana.Loki;
using System.Reflection;

namespace Auth.Api.Configurations;

public static class ObservabilityConfiguration
{
    public const string ServiceName = "auth-api";

    public static void AddOpenTelemetry(this IServiceCollection services, IConfiguration configuration)
    {
        var resourceBuilder = GetResourceBuilder();

        services.AddOpenTelemetryTracing(telemetry =>
        {
            telemetry
                .AddSource(ServiceName)
                .SetResourceBuilder(resourceBuilder)
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddSqlClientInstrumentation()
                .SetSampler(new AlwaysOnSampler())
                .AddJaegerExporter(jaegerOptions =>
                {
                    jaegerOptions.AgentHost = configuration.GetValue<string>("Jaeger:Host");
                    jaegerOptions.AgentPort = configuration.GetValue<int>("Jaeger:Port");
                });
        });

        services.AddOpenTelemetryMetrics(builder =>
        {
            builder
                .SetResourceBuilder(resourceBuilder)
                .AddPrometheusExporter()
                .AddHttpClientInstrumentation()
                .AddAspNetCoreInstrumentation()
                .AddRuntimeInstrumentation();
        });
    }

    public static void AddSerilog(this IHostBuilder host)
    {
        var lokiLabels = new List<LokiLabel>
        {
            new LokiLabel { Key = "services", Value = ServiceName }
        };

        var propertiesLabels = new List<string> { "level", "MachineName" };

        host.UseSerilog((ctx, lc) => lc
            .MinimumLevel.Warning()
            .Enrich.WithMachineName()
            .WriteTo.GrafanaLoki("http://localhost:3100", lokiLabels, propertiesLabels)
            .WriteTo.Console()
        );
    }

    private static ResourceBuilder GetResourceBuilder()
    {
        var assemblyVersion = Assembly.GetExecutingAssembly().GetName().Version?.ToString() ?? "unknown";

        return ResourceBuilder
            .CreateDefault()
            .AddService(ServiceName, serviceVersion: assemblyVersion, serviceInstanceId: Environment.MachineName);
    }
}
