using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger
{
    public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
    {
        readonly IApiVersionDescriptionProvider apiVersionDescriptionProvider;

        public ConfigureSwaggerOptions(IApiVersionDescriptionProvider apiVersionDescriptionProvider)
        {
            this.apiVersionDescriptionProvider = apiVersionDescriptionProvider;
        }

        static OpenApiInfo CreateInfoForApiVersion(ApiVersionDescription description)
        {
            var info = new OpenApiInfo
            {
                Version = description.ApiVersion.ToString(),
                Title = "Pacagroup Technology Services API Market",
                Description = "A simple example ASP.NET Core Web API",
                TermsOfService = new Uri("https://pacagroup.com/terms"),
                Contact = new OpenApiContact
                {
                    Name = "Alex Espejo",
                    Email = "alex.espejo.c@gmail.com",
                    Url = new Uri("https://pacagroup.com/contact")
                },
                License = new OpenApiLicense
                {
                    Name = "Use under LICX",
                    Url = new Uri("https://pacagroup.com/licence")
                }
            };

            return info;
        }

        public void Configure(SwaggerGenOptions options)
        {
            foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
            {
                options.SwaggerDoc(description.GroupName, CreateInfoForApiVersion(description));
            }
        }
    }
}
