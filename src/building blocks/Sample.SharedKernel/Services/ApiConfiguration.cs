using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;

namespace Sample.SharedKernel.Services
{
    public static class ApiConfiguration
    {
        private const string DefaultCorsPolicy = "Total";
        public static IServiceCollection AddApiConfiguration(this IServiceCollection services, bool enableCors = false)
        {
            services.AddControllers();

            if (enableCors)
            {
                services.AddCors(options =>
                {
                    options.AddPolicy(DefaultCorsPolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
                });
            }

            return services;
        }

        public static IApplicationBuilder UseApiConfiguration(this IApplicationBuilder app, IWebHostEnvironment env, bool enableCors = false, bool enableAuthentication = false)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            if (enableCors)
                app.UseCors(DefaultCorsPolicy);

            if (enableAuthentication)
                //app.UseAuthenticationConfiguration();

            app.UseMiddleware<GlobalExceptionHandlerMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            return app;
        }

        public static WebApplicationBuilder AddConfigurationFile(this WebApplicationBuilder builder)
        {
            builder.Configuration
                .SetBasePath(builder.Environment.ContentRootPath)
                .AddJsonFile(path: "appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile(path: $"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder;
        }
    }
}
