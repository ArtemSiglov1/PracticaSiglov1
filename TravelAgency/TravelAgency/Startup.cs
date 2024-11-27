using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using TravelAgency.DAL;
using TravelAgency.Interface;
using TravelAgency.Service;

namespace TravelAgency
{
    public class Startup
    {
        private IConfigurationRoot root;

        public Startup(IWebHostEnvironment hosting)
        {
            root = new ConfigurationBuilder()
                .SetBasePath(hosting.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();
        }

       

        public void ConfigureServices(IServiceCollection services)
        {
            // Добавляем DbContext с использованием строки подключения
            services.AddDbContext<DataContext>(options =>
                options.UseNpgsql(root.GetConnectionString("DefaultConnection")));
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                    options.AccessDeniedPath = new Microsoft.AspNetCore.Http.PathString("/Home/Login");
                });
            services.AddTransient<IAccountService, AccountService>();
            //services.AddTransient<ICarsCategory, CategoryRepository>();
            //services.AddTransient<ICars, MockCars>();
            //services.AddTransient<ICarsCategory, MockCategory>();
            //services.AddTransient<IDataModulService, DataService>();
            //services.AddTransient<IStorageService, StorageService>();
            //services.AddTransient<ISellerService, SellerService>();
            //services.AddTransient<IClientService, ClientService>();
            services.AddControllersWithViews();
        }



        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthorization();
            app.UseAuthentication();
            //app.UseMvc();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=SiteInformation}/{id?}");
                //endpoints.MapControllerRoute(
                //                name: "default",
                //                pattern: "{controller=Shops}/{action=Result}");
            });
        }
    }
}
