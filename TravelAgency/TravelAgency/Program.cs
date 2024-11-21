using Microsoft.AspNetCore;
using TravelAgency;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;

CreateWedHost(args).Build().Run();
        static IWebHostBuilder CreateWedHost(string[] args) => WebHost.CreateDefaultBuilder(args)
            .UseStartup<Startup>();
    

