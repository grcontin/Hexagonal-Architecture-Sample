using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Sample.SharedKernel.MediatR.Behaviors;
using System.Reflection;

namespace Sample.SharedKernel.MediatR
{
    public static class MediatRConfiguration
    {
        public static IServiceCollection AddMediatRServices(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddMediatR(configure =>
            {
                configure.RegisterServicesFromAssemblies(assemblies);
            });

            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(LoggingPipelineBehavior<,>));

            return services;
        }
    }
}
