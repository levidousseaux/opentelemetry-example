using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Auth.Api;

public sealed class HealthCheck : IHealthCheck
{
    public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(HealthCheckResult.Healthy());
    }
}