using System;
using System.IO;
using System.Reflection;
using BasketApi.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace BasketApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            SetupDependencyInjections(services);
            services.AddMvc();
            services.AddSwaggerGen(ConfigureSwaggerGenerationSetup);
        }

        private static void SetupDependencyInjections(IServiceCollection services)
        {
            services.AddScoped<BasketService>();
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

        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseMvc();
            app.UseHttpsRedirection();
            app.UseSwagger().UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BasketApi"));
        }
    }
}
