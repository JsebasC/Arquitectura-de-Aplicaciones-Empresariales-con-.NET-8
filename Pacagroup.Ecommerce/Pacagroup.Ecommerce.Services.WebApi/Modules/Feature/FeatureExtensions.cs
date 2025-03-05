using System.Text.Json.Serialization;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Feature
{
    public static class FeatureExtensions
    {
        public static IServiceCollection AddFeature(this IServiceCollection services, IConfiguration configuration)
        {

            string[]? corsOrigenes = configuration.GetSection("CorsOrigenes").Get<string[]>();
            services.AddCors(op =>
            {
                op.AddPolicy("MyPoliceCors", builder =>
                {
                    builder.WithOrigins(corsOrigenes!)
                    .WithHeaders("authorization", "accept", "content-type", "origin")
                    .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
                });
            });

            services.AddControllers().AddJsonOptions(opts =>
            {
                var enumConverter = new JsonStringEnumConverter();
                opts.JsonSerializerOptions.Converters.Add(enumConverter);
            });

            services.AddRequestTimeouts(options =>
            {
                options.DefaultPolicy = 
                    new Microsoft.AspNetCore.Http.Timeouts.RequestTimeoutPolicy { Timeout = TimeSpan.FromMilliseconds(1500) };
                options.AddPolicy("CustomPolicy", TimeSpan.FromMilliseconds(2000));
            });

            return services;
        }
    }
}
