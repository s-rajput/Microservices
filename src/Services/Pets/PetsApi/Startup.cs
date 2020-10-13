using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PetsApi.Models;
using PetsApi.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using AutoMapper;
using System.Text;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using PetsApi.Controllers;
using PetsApi.Config;
using Polly;
using System.Net.Http;
using Polly.Extensions.Http;
using Microsoft.AspNetCore.Http;
using PetsApi.Services.Implementations;
using System.IdentityModel.Tokens.Jwt;
using PetsApi.Extensions;

namespace PetsApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration) { Configuration = configuration; }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCorsPolicies();
            services.AddControllers();
            //register services
           
            services.AddSingleton<IPetsApi, PetsApiSvc>();
            //swagger
            services.AddCustomSwagger();
            //app settings
            services.Configure<ConfigSettings>(Configuration.GetSection("Settings"));  
            
            //all services ICATS, IPETS are registered here for httpclient lifecycle optimisation
            services.AddApplicationServices();
            services.AddAuthenticationDetails();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("AllowAll");
            app.ConfigureRoutingAndAuth();
            //swagger 
            app.ConfigureSwagger(Configuration);
        }

    }   
}