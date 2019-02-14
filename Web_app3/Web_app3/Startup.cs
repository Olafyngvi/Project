using AutoServis.EF;
using AutoServis.Helper;
using AutoServis.Hubs;
using AutoServis.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ReflectionIT.Mvc.Paging;

namespace Web_app3
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
        Configuration = configuration;
        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSignalR();
            services.AddMvc();
           
            services.AddDbContext<MojContext>(options =>
            options.UseSqlServer(Configuration.GetConnectionString("MojConnectionString")));
            services.AddMemoryCache();
            services.AddSession();
            services.AddDistributedMemoryCache();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
         
            services.AddScoped<IOrderRepository, OrderRepository>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
            services.AddPaging();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSession();
            app.UseStaticFiles();
            app.UseSignalR(routes =>  // <-- SignalR
            {
                routes.MapHub<ReportsPublisher>("/reportsPublisher");
            });
            app.UseMvc(routes => {

             
                    routes.MapRoute(
                      name: "areas",
                      template: "{area:exists}/{controller=Home}/{action=Index}/{id?}"
                    );
               
                routes.MapRoute("Login", "Login/{*autent}",
                  defaults: new { controller = "Autentifikacija", action = "Index" }
                  );
                routes.MapRoute(
                name: null,
                template: "{mar}/Page{productPage:int}",
                defaults: new { controller = "Dijelovi", action = "List" }
                );
                routes.MapRoute(
                name: null,
                template: "{mod}/Page{productPage:int}",
                defaults: new { controller = "Dijelovi", action = "List" }
                );
                routes.MapRoute(
                name: null,
                template: "Page{productPage:int}",
                defaults: new
                {
                    controller = "Dijelovi",
                    action = "List",
                    productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "{mar}",
                defaults: new
                {
                    controller = "Dijelovi",
                    action = "List",
                    productPage = 1
                });
                routes.MapRoute(
                name: null,
                template: "{mod}",
                defaults: new
                {
                    controller = "Dijelovi",
                    action = "List",
                    productPage = 1
                }
                );
                routes.MapRoute(
                name: null,
                template: "",
                defaults: new
                {
                    controller = "Home",
                    action = "Index",
                    productPage = 1
                });
             

           

            
                routes.MapRoute(name: null, template: "{controller}/{action}/{id?}");
              

                

            });
            app.UseExceptionHandler();
            app.UseDeveloperExceptionPage();

         
        }
    }
}
