using AutoMapper;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Sample.SharedKernel.AutoMapper
{
    public static class AutoMapperConfiguration
    {
        public static IServiceCollection AddAutoMapperServices(this IServiceCollection services, Assembly[] assemblies)
        {
            services.AddAutoMapper(assemblies);

            return services;
        }

        public static IServiceCollection AddAutoMapperServices<TProfile>(this IServiceCollection services, TProfile profile)
            where TProfile : Profile, new()
        {
            services.AddAutoMapper(configuration => configuration.AddProfile(new TProfile()));
            return services;
        }
    }
}
