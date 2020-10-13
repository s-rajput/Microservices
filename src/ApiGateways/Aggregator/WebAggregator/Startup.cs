using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http; 
using WebAggregator.Config;
using WebAggregator.Extensions;
using WebAggregator.Services;
using WebAggregator.Services.Implementations;

namespace WebAggregator
{
    public class Startup
    {

        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsPolicies();
            services.AddControllers();
            services.AddCustomSwagger(Configuration);
            //configsettings
            services.Configure<UrlsConfig>(Configuration.GetSection("Services"));
            services.Configure<IdentityConfig>(Configuration.GetSection("IdentitySettings"));
            //app services
            services.AddApplicationServices(); 
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll"); 
            app.ConfigureRouting();
            //swagger 
            app.ConfigureSwagger(Configuration);
            System.Net.ServicePointManager.ServerCertificateValidationCallback += (sender, certificate, chain, sslPolicyErrors) => true;
        }
    }   
}
