using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace middlewareDemo
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
            services.AddControllersWithViews();
           //configure the session options
            services.AddSession(
                options => options.IdleTimeout 
                = TimeSpan.FromMinutes(1)//set session timeout
                ); ;
            //add mvc services
            services.AddMvc();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //this middleware is used to report the application runtime errors in developement envoirnment
                app.UseDeveloperExceptionPage();
            }
            else
            {//catches exception thorwn in production envoirment
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                //add the strict transport security header
                app.UseHsts();
            }
            //is used to redirects http request to https
            app.UseHttpsRedirection();
            //is used to return static files  like css,images,js,bootstrap etc..
            app.UseStaticFiles();
            //is used to server session object
            app.UseSession();
            //is used to route requests.
            app.UseRouting();
            //is used to authorize a user to access secure resources.
            app.UseAuthorization();
            //is used to add razor pages endpoints to the request pipeline.
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                    endpoints.MapControllerRoute(
                    name: "HomePrivacy",
                    pattern: "{controller=Home}/{action=Privacy}/{id?}");
            });
        }
    }
}
