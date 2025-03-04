using Asp.Versioning.ApiExplorer;
using HealthChecks.UI.Client;
using Pacagroup.Ecommerce.Application.Features;
using Pacagroup.Ecommerce.Infraestructure;
using Pacagroup.Ecommerce.Persistence;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Authentication;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Feature;
using Pacagroup.Ecommerce.Services.WebApi.Modules.HealthCheck;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Injection;
using Pacagroup.Ecommerce.Services.WebApi.Modules.RateLimiter;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Redis;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Swagger;
using Pacagroup.Ecommerce.Services.WebApi.Modules.Versioning;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

builder.Services.AddFeature(builder.Configuration);
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddInjection(builder.Configuration);
builder.Services.AddAuthenticationExtension(builder.Configuration);
builder.Services.AddVersioning();
builder.Services.AddSwagger();
builder.Services.AddHealthCheck(builder.Configuration);
builder.Services.AddRedisCache(builder.Configuration);
builder.Services.AddRateLimiting(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        // build a swagger endpoint for each discovered API version
        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            c.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json", description.GroupName.ToUpperInvariant());
        }
    });

    //api-docs
    app.UseReDoc(options =>
    {
        var apiVersionDescriptionProvider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();

        foreach (var description in apiVersionDescriptionProvider.ApiVersionDescriptions)
        {
            options.DocumentTitle = "Pacagroup Technology Services API Market";
            options.SpecUrl = $"/swagger/{description.GroupName}/swagger.json";
        }
    });
}

app.UseCors("MyPoliceCors");
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseEndpoints(_ => { });
app.MapControllers();
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

app.Run();
