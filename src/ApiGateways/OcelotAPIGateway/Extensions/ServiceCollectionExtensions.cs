using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using MMLib.Ocelot.Provider.AppConfiguration;
using Ocelot.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGatewayOcelot.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddOcelotServices(this IServiceCollection services)
        {
            services.AddOcelot().AddAppConfiguration();
            return services;
        }

        public static IServiceCollection AddSwaggerServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSwaggerForOcelot(configuration);
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "My API", Version = "v1" });
            });
            return services;
        }

    }
}
