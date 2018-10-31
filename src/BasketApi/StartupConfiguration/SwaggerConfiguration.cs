using System;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BasketApi.StartupConfiguration
{
    public static class SwaggerConfiguration
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddSwaggerGen(ConfigureSwaggerGenerationSetup);
        }

        private static void ConfigureSwaggerGenerationSetup(SwaggerGenOptions options)
        {
            var info = new Info
            {
                Title = "BasketApi",
                Description = "In memory basket functionality",
                Version = "v1"
            };
            options.SwaggerDoc("v1", info);
            IncludeXmlCommentsInSwagger(options);
        }

        private static void IncludeXmlCommentsInSwagger(SwaggerGenOptions options)
        {
            var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
            var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
            if (File.Exists(xmlPath))
            {
                options.IncludeXmlComments(xmlPath, true);
            }
        }

        public static void ConfigureApp(IApplicationBuilder app)
        {
            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketApi"));
        }
    }
}