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

            return services;
        }
    }
}
