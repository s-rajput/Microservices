using Microsoft.AspNetCore.Builder;
using Ocelot.Middleware;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace APIGatewayOcelot.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureRouting(this IApplicationBuilder app)
        {
            app.UseRouting(); 
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            app.UseStaticFiles();
            return app;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerForOcelotUI(opt =>
                {
                    opt.DownstreamSwaggerHeaders = new[]
                    {
                            new KeyValuePair<string, string>("Key", "Value"),
                            new KeyValuePair<string, string>("Key2", "Value2"),
                        };
                    opt.ServerOcelot = "/siteName/apigateway";
                })
               .UseOcelot()
               .Wait();
            return app;
        }
    }
}
