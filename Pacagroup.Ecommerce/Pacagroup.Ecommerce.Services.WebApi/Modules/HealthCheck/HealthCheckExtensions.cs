namespace Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck
{
    public static class HealthCheckExtensions
    {
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("Northwind"), tags: ["database"])
                .AddRedis(configuration.GetConnectionString("RedisConnection"), tags: ["cache"])
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: ["custom"]);

            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }
    }
}
