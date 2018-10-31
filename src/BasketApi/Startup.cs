using BasketApi.StartupConfiguration;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace BasketApi
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            InjectionConfiguration.Configure(services);
            services.AddMvc();
            SwaggerConfiguration.ConfigureServices(services);
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
            SwaggerConfiguration.ConfigureApp(app);
        }
    }
}
