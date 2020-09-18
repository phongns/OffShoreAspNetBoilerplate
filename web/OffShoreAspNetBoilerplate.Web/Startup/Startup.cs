using System;
using Abp.AspNetCore;
using Abp.AspNetCore.Mvc.Antiforgery;
using Abp.ZeroCore.Authorization.Roles;
using Abp.ZeroCore.Authorization.Users;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using OffShoreAspNetBoilerplate.Core.Identity;
using OffShoreAspNetBoilerplate.Web.Core.Authentication.JwtBearer;
using OffShoreAspNetBoilerplate.Web.Core.Configuration;

namespace OffShoreAspNetBoilerplate.Web.Startup
{
    public class Startup
    {
        private readonly IConfigurationRoot _appConfiguration;

        public Startup(IWebHostEnvironment env/*IConfiguration configuration*/)
        {
            //Configuration = configuration;
            _appConfiguration = env.GetAppConfiguration();
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void /*IServiceProvider*/ ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews(
                options =>
                {
                    //options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
                    //options.Filters.Add(new AbpAutoValidateAntiforgeryTokenAttribute());

                })
                .AddRazorRuntimeCompilation()
                .AddNewtonsoftJson();

            services.AddIdentity<AbpUser, AbpRole>()
                .AddDefaultTokenProviders();

            //IdentityRegistrar.Register(services);
            AuthConfigurer.Configure(services, _appConfiguration);

            //return services.AddAbp<AbpProjectNameWebMvcModule>(
            //    //// Configure Log4Net logging
            //    //options => options.IocManager.IocContainer.AddFacility<LoggingFacility>(
            //    //    f => f.UseAbpLog4Net().WithConfig("log4net.config")
            //    //)
            //);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //app.UseAbp(); // Initializes ABP framework.

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();

            //app.UseJwtTokenMiddleware();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                //endpoints.MapRazorPages();
                endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapControllerRoute("defaultWithArea", "{area}/{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
