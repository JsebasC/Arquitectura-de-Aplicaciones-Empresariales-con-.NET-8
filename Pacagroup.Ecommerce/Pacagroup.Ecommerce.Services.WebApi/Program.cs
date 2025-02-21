using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

ConfigurationManager configuraciones = builder.Configuration;

// Add services to the container.
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
    {
        Title = "Mi API",
        Version = "v1",
        Description = "A simple example ASP.NET Core Web API",
        Contact = new Microsoft.OpenApi.Models.OpenApiContact
        {
            Name = "Johan Cuellar",
            Email = ""
        }
    });
});

builder.Services.AddAutoMapper(typeof(MappingsProfile));

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();

string[]? corsOrigenes = configuraciones.GetSection("CorsOrigenes").Get<string[]>();
builder.Services.AddCors(op =>
{
    op.AddPolicy("MyPoliceCors", builder =>
    {
        builder.WithOrigins(corsOrigenes!)
        .WithHeaders("authorization", "accept", "content-type", "origin")
        .WithMethods("GET", "PUT", "POST", "DELETE", "OPTIONS");
    });
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Mi API v1"));
}
app.UseCors("MyPoliceCors");
app.UseAuthorization();
app.MapControllers();
app.Run();
