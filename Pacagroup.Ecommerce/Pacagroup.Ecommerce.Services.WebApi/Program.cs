using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Pacagroup.Ecommerce.Application.Interface;
using Pacagroup.Ecommerce.Application.Main;
using Pacagroup.Ecommerce.Domain.Core;
using Pacagroup.Ecommerce.Domain.Interface;
using Pacagroup.Ecommerce.Infrastructure.Data;
using Pacagroup.Ecommerce.Infrastructure.Interface;
using Pacagroup.Ecommerce.Infrastructure.Repository;
using Pacagroup.Ecommerce.Services.WebApi.Helpers;
using Pacagroup.Ecommerce.Transversal.Common;
using Pacagroup.Ecommerce.Transversal.Mapper;
using System.Text;

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

    c.AddSecurityDefinition("Authorization", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
    {
        Description = "Ingrese un token válido",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Authorization",                    
                }
            },
            new string[] { }
        }
    });

});

builder.Services.AddAutoMapper(typeof(MappingsProfile));
var appSettings = configuraciones.GetSection("Config").Get<AppSettings>();

builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>();
builder.Services.AddScoped<ICustomersApplication, CustomersApplication>();
builder.Services.AddScoped<ICustomersDomain, CustomersDomain>();
builder.Services.AddScoped<ICustomersRepository, CustomersRepository>();
builder.Services.AddScoped<IUsersApplication, UsersApplication>();
builder.Services.AddScoped<IUsersDomain, UsersDomain>();
builder.Services.AddScoped<IUsersRepository, UsersRepository>();
builder.Services.AddSingleton(appSettings!);

var key = Encoding.ASCII.GetBytes(appSettings.Secret);
var issuer = appSettings.Issuer;
var audience = appSettings.Audience;

builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(jwt =>
{
    jwt.Events = new JwtBearerEvents
    {
        OnTokenValidated = context =>
        {
            var userId = int.Parse(context.Principal.Identity.Name);
            return Task.CompletedTask;
        },
        OnAuthenticationFailed = context =>
        {
            if (context.Exception.GetType() == typeof(SecurityTokenExpiredException))
            {
                context.Response.Headers.Add("Token-Expired", "true");
            }
            return Task.CompletedTask;
        }
    };
    jwt.RequireHttpsMetadata = false;
    jwt.SaveToken = false;
    jwt.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = true,
        ValidIssuer = issuer,
        ValidateAudience = true,
        ValidAudience = audience,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };

});


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
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();
app.Run();
