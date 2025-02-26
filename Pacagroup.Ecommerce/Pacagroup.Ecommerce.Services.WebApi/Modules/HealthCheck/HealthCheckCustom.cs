using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck
{
    public class HealthCheckCustom : IHealthCheck
    {
        private readonly Random _random = new();
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            var tiempoRespuesta = _random.Next(1, 300);
            if(tiempoRespuesta < 100)
            {
                return Task.FromResult(HealthCheckResult.Healthy("Custom"));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("Custom"));
        }
    }
}
