using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Features.Categories;
using Pacagroup.Ecommerce.Application.Features.Customers;
using Pacagroup.Ecommerce.Application.Features.Users;
using Pacagroup.Ecommerce.Application.Interface.Features;

namespace Pacagroup.Ecommerce.Application.Features
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();

            return services;
        }
    }
}
