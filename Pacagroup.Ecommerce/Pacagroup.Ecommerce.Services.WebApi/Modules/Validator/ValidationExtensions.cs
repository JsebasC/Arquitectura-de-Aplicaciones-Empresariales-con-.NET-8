﻿using Pacagroup.Ecommerce.Application.Validator;

namespace Pacagroup.Ecommerce.Services.WebApi.Modules.Validator
{
    public static class ValidationExtensions
    {
        public static IServiceCollection AddValidator(this IServiceCollection services)
        {
            services.AddTransient<UsersDtoValidator>();

            return services;
        }
    }
}
