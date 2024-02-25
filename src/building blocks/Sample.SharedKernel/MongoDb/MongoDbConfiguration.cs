using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.Net.Sockets;

namespace Sample.SharedKernel.MongoDb
{
    public static class MongoDbConfiguration
    {
        public static IServiceCollection AddMongoDbConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            var section = configuration.GetSection("MongoDbConfiguration");
            var connectionString = section["ConnectionString"];
            var databaseName = section["DatabaseName"];

            var settings = MongoClientSettings.FromConnectionString(connectionString);

            static void SocketConfigurator(Socket s) => s.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.KeepAlive, true);

            settings.SocketTimeout = TimeSpan.FromMinutes(1);
            settings.ConnectTimeout = TimeSpan.FromSeconds(60);
            settings.MaxConnectionIdleTime = TimeSpan.FromSeconds(20);
            settings.ClusterConfigurator = builder => builder.ConfigureTcp(tcp => tcp.With(socketConfigurator: (Action<Socket>)SocketConfigurator));

            var mongoClient = new MongoClient(settings);

            services.AddSingleton(t => new MongoDbContext(databaseName, mongoClient));

            services.AddSingleton<IMongoClient>(_ => new MongoClient(connectionString));
            services.AddSingleton(provider => provider.GetRequiredService<IMongoClient>().GetDatabase(databaseName));

            return services;
        }
    }
}
