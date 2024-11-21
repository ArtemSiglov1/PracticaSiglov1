using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;



namespace TravelAgency
{
    public class Startup
    {
        //private IConfigurationRoot root;

        //public Startup(IWebHostEnvironment hosting)
        //{
        //    root = new ConfigurationBuilder()
        //        .SetBasePath(hosting.ContentRootPath)
        //        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        //        .Build();
        //}

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string conn = "Host=localhost;Port=5432;Database=BookStore;Username=postgres;Password=111111";
            // Добавляем DbContext с использованием строки подключения
            services.AddDbContext<DbContext>(options =>
                options.UseNpgsql(conn));
            //services.AddTransient<ICars, CarRepository>();
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
