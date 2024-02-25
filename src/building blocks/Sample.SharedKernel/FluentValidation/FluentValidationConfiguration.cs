using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sample.SharedKernel.FluentValidation
{
    public static class FluentValidationConfiguration
    {
        public static IServiceCollection AddFluentValidationValidators(this IServiceCollection services, string validatorsAssembly)
        {
            services.AddValidatorsFromAssembly(Assembly.Load(validatorsAssembly));

            return services;
        }
    }
}
