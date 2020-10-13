using System.Collections.Generic;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Microsoft.Extensions.Hosting;
using MMLib.Ocelot.Provider.AppConfiguration;
using Microsoft.OpenApi.Models;
using APIGatewayOcelot.Extensions;

namespace OcelotAPIGateway
{
    public class Startup
    {
        private readonly IConfiguration _cfg;
        public Startup(IConfiguration configuration) { _cfg = configuration; }
        
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOcelotServices();
            services.AddSwaggerServices(_cfg);
            services.AddControllers();
        }
         
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.ConfigureRouting();
            app.UseSwagger();
            app.ConfigureSwagger();
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }
    } 
}
