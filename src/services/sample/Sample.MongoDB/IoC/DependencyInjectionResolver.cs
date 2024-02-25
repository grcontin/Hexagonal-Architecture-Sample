using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sample.Domain.Contratcts;
using Sample.MongoDB.Repositories;
using Sample.SharedKernel.MongoDb;

namespace Sample.MongoDB.IoC
{
    public static class DependencyInjectionResolver
    {
        public static IServiceCollection AddMongoDbAdapter(this IServiceCollection services, IConfiguration configuration) 
        {
            services.AddScoped<ITodoItemRepository, TodoItemRepository>();
            services.AddMongoDbConfiguration(configuration);

            return services;
        }
    }
}
