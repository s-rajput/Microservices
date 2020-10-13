using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PetsApi.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static IApplicationBuilder ConfigureRoutingAndAuth(this IApplicationBuilder app)
        {
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
            return app;
        }

        public static IApplicationBuilder ConfigureSwagger(this IApplicationBuilder app, IConfiguration Configuration)
        {
            var vd = Configuration["VirtualDirectory"];
            app.UseSwagger()
               .UseSwaggerUI(c =>
               {
                   c.SwaggerEndpoint($"{ (!string.IsNullOrEmpty(vd) ? vd : string.Empty) }/swagger/v1/swagger.json", "TEST.API V1");
                   c.InjectStylesheet("/swagger-ui/custom.css");
               });

            
            return app;
        }

    }
}
