using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Base_Classes;
using Core.Entities;
using Core.Interfaces;
using Infrastructure;
using Infrastructure.Repository;
using Infrastructure.UnitOfWork;
using Infrastructure.UnitOfWork.MyUOW;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


namespace Web
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;//to acess appsettings.json file
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<ApplicationDbContext>(options =>
            //   options.UseSqlServer(
            //       Configuration.GetConnectionString("DefaultConnection")));

            var ret=services.AddDbContext<DataContext>(options =>
            {
                //options.EnableSensitiveDataLogging();
                options.UseSqlServer(
                    Configuration.GetConnectionString("SqlServerConn"));
              } 
               ) ;

            

            //services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<DataContext>();

            
            
            


            services.AddControllersWithViews(); 
            services.AddRazorPages();




            services.AddScoped<IUnitOfWork<PortofolioUOW>, PortofolioUOW>();
            services.AddScoped<IUnitOfWork<TestUOW>, TestUOW>();
            
            
            //the next 2 lines work , they help me to inject PortrofolioUOW directly to the controller not injecting the interface ,
            //but i am not sure is "context" is the same DataContext created or not
            //so i will still injecting the interrface as the previous 2 lines

            //        var context = services.BuildServiceProvider().GetService<DataContext>();
            //      services.AddScoped<IUnitOfWork>(sp => new PortofolioUOW(context));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Portofolio}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
