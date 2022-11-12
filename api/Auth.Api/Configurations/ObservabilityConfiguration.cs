using Prometheus;
using Prometheus.DotNetRuntime;
using Serilog;
using Serilog.Sinks.Grafana.Loki;

namespace Auth.Api.Configurations;

public static class ObservabilityConfiguration
{
    public static void AddPrometheus(this IServiceCollection services)
    {
        services.AddHealthChecks().AddCheck<HealthCheck>(nameof(HealthCheck)).ForwardToPrometheus();
        services.AddHttpClient("").UseHttpClientMetrics();
        IDisposable collector = DotNetRuntimeStatsBuilder.Default().StartCollecting();
    }

    public static void AddLoki(this IHostBuilder host)
    {
        host.UseSerilog((ctx, lc) => lc
            .WriteTo.GrafanaLoki("http://loki:3100", new List<LokiLabel> { new() { Key = "dotnet", Value = "auth-api" } })
            .WriteTo.Console()
        );
    }

    public static void UsePrometheus(this WebApplication app)
    {
        app.UseHttpMetrics();
        app.UseMetricServer();
    }
}
