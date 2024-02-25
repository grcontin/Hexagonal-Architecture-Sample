using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;

namespace Sample.SharedKernel.Swagger
{
    public static class SwaggerConfigutarion
    {
        public static IServiceCollection AddSwaggerConfiguration(this IServiceCollection services,
                                                                 string version,
                                                                 string title,
                                                                 string description,
                                                                 string contactName,
                                                                 string contactEmail,
                                                                 string licenseType,
                                                                 string licenseUrl,
                                                                 bool useTokenInHeader = false)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(version,
                             new OpenApiInfo
                             {
                                 Title = title,
                                 Description = description,
                                 Contact = new OpenApiContact() { Name = contactName, Email = contactEmail },
                                 License = new OpenApiLicense() { Name = licenseType, Url = new Uri(licenseUrl) }
                             });

                if (useTokenInHeader)
                {
                    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                    {
                        Description = "Insira o token JWT",
                        Name = "Authorization",
                        Scheme = "Bearer",
                        BearerFormat = "JWT",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http
                    });

                    options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = JwtBearerDefaults.AuthenticationScheme
                                }
                            },
                        Array.Empty<string>()
                        }
                    });
                }

            });

            return services;
        }

        public static IApplicationBuilder UseSwaggerConfiguration(this IApplicationBuilder app, string version)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint($"/swagger/{version}/swagger.json", version);
            });

            return app;
        }
    }
}
