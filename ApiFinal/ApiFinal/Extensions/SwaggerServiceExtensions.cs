using Microsoft.OpenApi.Models;

namespace ApiFinal.Extensions
{
    public static class SwaggerServiceExtensions
    {
        public static IServiceCollection AddSwaggerDocumentation(this IServiceCollection services)
        {
            //Services.AddSwaggerGen();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title ="SkiNet Api" ,Version ="v1"});

            });
            return services;
        }

        public static IApplicationBuilder UseSwaggerDocumentation( this IApplicationBuilder app )
        {
            app.UseSwagger();
            app.UseSwaggerUI(c => { c.SwaggerEndpoint("/swagger/v1/swagger.json", "Skinet api v1"); });
            return app;
        }
        
    }
}
