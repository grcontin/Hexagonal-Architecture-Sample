using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.MongoDB.IoC;

namespace Sample.Application
{
    public static class ApplicationModule
    {
        public static IServiceCollection AddAdapters(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddMongoDbAdapter(configuration);

            return services;
        }
    }
}
