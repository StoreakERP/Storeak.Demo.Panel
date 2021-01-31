using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Storeak.Demo.Panel.Service;
using Storeak.Demo.Panel.Service.User;
using Storeak.Demo.Panel.StoreType;
using StoreakApiService.Core.Projects;

namespace Storeak.Demo.Panel
{
    public class Startup : StoreakApiService.Core.Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment env) : base(configuration, env)
        {
            ApiProjectSettings.ProjectId = 0;
            LoadFrontEndAssets += Startup_LoadFrontEndAssets;
            ApiProjectSettings.EndType = ProjectEndType.FrontEnd;
            ApiProjectSettings.UseSession = true;
        }

        private void Startup_LoadFrontEndAssets()
        {
            ApiProjectSettings.AdminAssetPath = "https://www.storeakmedia.com/theme/"; // must all small latter or all files will be not found on blob
        }

        public override void ConfigureServices(IServiceCollection services)
        {
            base.ConfigureServices(services);

            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            base.ConfigureAutoMapper(services, new MapperProfile());

            services.AddScoped<ResourceMessages>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IStoreTypeService, StoreTypeService>();
        }

        public override void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider ServiceProvider)
        {
            if (!env.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }

            base.Configure(app, env, ServiceProvider);

            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Account}/{action=Login}/{id?}");
            });
        }
    }
}
