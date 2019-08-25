using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using WebMotors.Application.Services;
using WebMotors.Data.Context;
using WebMotors.Data.Implementation;
using WebMotors.Data.Implementation.Base;
using WebMotors.DI;
using WebMotors.Domain.Interfaces;
using WebMotors.Domain.Interfaces.Services;

namespace WebMotors.Api
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });


            Injetor.InjetarDependecias(services);

            //services.AddTransient<Contexto>();
            //services.AddTransient<IUnitOfWork, IUnitOfWork>();
            //services.AddTransient(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            //services.AddTransient<IAnuncioRepository, AnuncioRepository>();

            //services.AddTransient(typeof(IServiceBase<>), typeof(IServiceBase<>));
            //services.AddTransient<IAnuncioService, AnuncioService>();


            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
