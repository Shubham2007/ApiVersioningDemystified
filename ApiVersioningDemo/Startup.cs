using ApiVersioningDemo.Controllers;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.Versioning.Conventions;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ApiVersioningDemo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddApiVersioning(options =>
            {
                options.DefaultApiVersion = new ApiVersion(1, 0);
                //options.AssumeDefaultVersionWhenUnspecified = true;
                options.ReportApiVersions = true;
                //options.ApiVersionReader = new QueryStringApiVersionReader("v");
                //options.ApiVersionReader = new HeaderApiVersionReader("version");
                options.ApiVersionReader = ApiVersionReader.Combine(
                    new QueryStringApiVersionReader("v"),
                    new QueryStringApiVersionReader("version"),
                    new HeaderApiVersionReader("v"),
                    new UrlSegmentApiVersionReader()
                    );

                //options.ApiVersionReader = new UrlSegmentApiVersionReader();
                options.Conventions.Controller<WeatherForecastController>()
                    .HasApiVersion(3, 0);
                    //.Action(c => c.GetV3_0()).MapToApiVersion(1, 1);
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
