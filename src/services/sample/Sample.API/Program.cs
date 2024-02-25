using Sample.Application;
using Sample.SharedKernel.AutoMapper;
using Sample.SharedKernel.FluentValidation;
using Sample.SharedKernel.MediatR;
using Sample.SharedKernel.Serilog;
using Sample.SharedKernel.Services;
using Sample.SharedKernel.Swagger;
using Serilog;
using System.Net;

namespace Sample.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                var builder = WebApplication.CreateBuilder(args);

                builder.WebHost.UseKestrel(options =>
                {
                    int httpPort = int.Parse(builder.Configuration["HttpPort"]);

                    options.Listen(IPAddress.Any, httpPort, configurations =>
                    {
                        configurations.Protocols = Microsoft.AspNetCore.Server.Kestrel.Core.HttpProtocols.Http1AndHttp2;
                    });
                });

                builder.AddConfigurationFile();

                string apiCurrentVersion = builder.Configuration["ApiCurrentVersion"];

                builder.Services.AddApiConfiguration(enableCors: false);
                builder.Services.AddAdapters(builder.Configuration);
                builder.Services.AddMediatRServices(AppDomain.CurrentDomain.GetAssemblies());
                builder.Services.AddFluentValidationValidators(builder.Configuration["FluentValidationValidatorsAssemblyName"]);
                builder.Services.AddAutoMapperServices(AppDomain.CurrentDomain.GetAssemblies());
                builder.AddSerilogConfiguration(builder.Configuration);
                builder.Services.AddSwaggerConfiguration(apiCurrentVersion,
                                                 "Hexagonal Architecture Sample API",
                                                 "API para Exemplo de uso da Arquitetura Hexagonal",
                                                 "Gabriel Contin",
                                                 "gabriel.rcontin@gmail.com",
                                                 "MIT",
                                                 "http://opensource.org/licenses./MIT");

                var app = builder.Build();

                app.UseSwaggerConfiguration(apiCurrentVersion);
                app.UseApiConfiguration(app.Environment, enableCors: false);

                app.Run();

            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "Host terminated unexpectedly");
            }
            finally
            {
                Log.Information("Server Shutting down...");
                Log.CloseAndFlush();
            }
        }
    }
}
