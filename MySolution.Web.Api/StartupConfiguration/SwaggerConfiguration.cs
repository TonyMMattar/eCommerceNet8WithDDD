using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using System;

namespace eCommerce.Web.Api.StartupConfiguration
{
    internal static class SwaggerConfiguration
    {
        #region Variables
        public const string NameApiSwagger = "My DDD Solution API";
        public const string EndpointSwaggerJson = "/swagger/v1/swagger.json";
        #endregion

        #region Methods
        /// <summary>
        /// https://docs.microsoft.com/en-us/aspnet/core/tutorials/getting-started-with-swashbuckle
        /// </summary>
        /// <param name="services"></param>
        public static void AddSwaggerGen(this IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var version = "v1";

                options.SwaggerDoc(version, new OpenApiInfo
                {
                    Title = $"{NameApiSwagger} {version}",
                    Version = version,
                    Description = "Solution in DDD (Domain Driven Design) architecture.",
                    /*Contact = new OpenApiContact
                    {
                        Name = "Company Name",
                        Url = new Uri(""),
                    }*/
                });

                // For JWT configuration (optional)
                /*
                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    Name = "Authorization",
                    Description = "Enter the token according to the model."
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        Array.Empty<string>()
                    }
                });*/
            });
        }
        #endregion
    }
}