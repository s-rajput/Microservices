using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using Polly;
using Polly.Extensions.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebAggregator.Config;
using WebAggregator.Services;
using WebAggregator.Services.Implementations;

namespace WebAggregator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddCorsPolicies(this IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddPolicy("AllowAll",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });
            return services;
        }
 
        public static IServiceCollection AddCustomSwagger(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSwaggerGen(options =>
            {
                options.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Web Clients Aggregator APIs",
                    Version = "v1",
                    Description = "The back end services HTTP API"
                });
            });
            return services;
        }

        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            try
            {
                //register all app services here
                //services.AddSingleton<IAglIdentitySvc, AglIdentitySvc>();

                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

                //register http services 
                //commented as handling by injection for testing 
                //.AddHttpMessageHandler<HttpClientAuthorizationDelegatingHandler>()
                // .AddPolicyHandler(GetRetryPolicy());
                // .AddPolicyHandler(GetCircuitBreakerPolicy()); 

                services.AddHttpClient<IAglCoreSvc, AglCoreSvc>();
                services.AddHttpClient<IAglIdentitySvc, AglIdentitySvc>();

            }
            catch (Exception e) { throw new Exception("Error1 " + e.Message, e); }

            return services;
        }


        static IAsyncPolicy<HttpResponseMessage> GetRetryPolicy()
        {
            return HttpPolicyExtensions
              .HandleTransientHttpError()
              .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound || msg.StatusCode == System.Net.HttpStatusCode.GatewayTimeout)
              .Or<Polly.Timeout.TimeoutRejectedException>()
              .WaitAndRetryAsync(2, retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)));

        }

        static IAsyncPolicy<HttpResponseMessage> GetCircuitBreakerPolicy()
        {
            return HttpPolicyExtensions
                .HandleTransientHttpError()
                .CircuitBreakerAsync(2, TimeSpan.FromSeconds(30));
        }

        static IAsyncPolicy<HttpResponseMessage> Timeout(int seconds = 25) =>
        Policy.TimeoutAsync<HttpResponseMessage>(TimeSpan.FromSeconds(seconds));
    }
}
