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

                var securitySchema = new OpenApiSecurityScheme
                {
                    Description ="JWt Auth Bearer Scheme",
                    Name = "Authorization",
                    In =ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                    Scheme ="bearer",
                    Reference =new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id ="Bearer"
                    }
                };
                c.AddSecurityDefinition("Bearer", securitySchema);
                var securityRequirment = new OpenApiSecurityRequirement {{securitySchema, new []
                {"Bearer"}}};
                c.AddSecurityRequirement(securityRequirment);
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
