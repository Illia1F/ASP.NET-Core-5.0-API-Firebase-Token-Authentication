namespace ASP.NET_Core_5._0_API_Firebase_Token_Authentication.Infrastructure.Extensions
{
    using Microsoft.AspNetCore.Builder;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.OpenApi.Models;

    using Infrastructure.Models;

    public static class SwaggerExtension
    {
        public static void AddSwaggerDocumentation(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc(appSettings.Version, new OpenApiInfo
                {
                    Title = appSettings.Title,
                    Description = appSettings.Decription,
                    Version = appSettings.Version
                });

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = @"Authorization header using the Bearer scheme. \r\n\r\n 
                                      Enter 'Bearer' [space] and then your token in the text input below.
                                      \r\n\r\nExample: 'Bearer [your token]'",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public static void UseSwaggerDocumentation(this IApplicationBuilder app)
        {
            app.UseSwagger();

            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "Firebase Token Authentication API V1");
                // To serve the Swagger UI at the app's root (https://localhost:<port>/)
                options.RoutePrefix = string.Empty;
            });
        }
    }
}
