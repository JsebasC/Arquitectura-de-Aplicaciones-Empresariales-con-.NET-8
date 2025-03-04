using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.Features.Categories;
using Pacagroup.Ecommerce.Application.Features.Customers;
using Pacagroup.Ecommerce.Application.Features.Discounts;
using Pacagroup.Ecommerce.Application.Features.Users;
using Pacagroup.Ecommerce.Application.Interface.Features;
using Pacagroup.Ecommerce.Application.Validator;
using System.Reflection;

namespace Pacagroup.Ecommerce.Application.Features
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountsApplication, DiscountsApplication>();

            services.AddTransient<UsersDtoValidator>();
            services.AddTransient<DiscountDtoValidator>();

            return services;
        }
    }
}
