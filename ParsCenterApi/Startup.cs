using Common;
using ElmahCore.Mvc;
using Entities.Models.BasicInformation;
using WebFramework.Middlewares;
using WebFramework.Configuration;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;
using ParsCenterApi.Models.BasicInformation;
using AutoMapper;

namespace ParsCenterApi
{
    public class Startup
    {
        private readonly SiteSettings _siteSetting;

        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;

            Mapper.Initialize(config =>
            {
                config.CreateMap<Country, CountryDto>().ReverseMap();
            });

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
