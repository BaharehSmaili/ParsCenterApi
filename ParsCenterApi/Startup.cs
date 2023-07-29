using Common;
using Data;
using Data.Interface;
using Data.Repositories;
using ElmahCore.Mvc;
using ElmahCore.Sql;
using Entities.Models.BasicInformation;
using Microsoft.EntityFrameworkCore;
using Services;
using WebFramework.Middlewares;
using WebFramework.Configuration;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using Microsoft.AspNetCore.Mvc.Authorization;

namespace ParsCenterApi
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            _siteSetting = configuration.GetSection(nameof(SiteSettings)).Get<SiteSettings>();
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.Configure<SiteSettings>(Configuration.GetSection(nameof(SiteSettings)));

            services.AddDbContext(Configuration);

            services.AddMinimalMvc();

            services.AddElmah(Configuration, _siteSetting);

            services.AddJwtAuthentication(_siteSetting.JwtSettings);

            return services.BuildAutofacServiceProvider();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCustomExceptionHandler();

            app.UseHsts();
           
            app.UseElmah();
            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}
