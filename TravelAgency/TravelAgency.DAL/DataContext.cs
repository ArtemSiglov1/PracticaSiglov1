using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using TravelAgency.Domain.Models;

namespace TravelAgency.DAL
{
    public class DataContext: Microsoft.EntityFrameworkCore.DbContext
    {
        protected readonly IConfiguration configuration;

        public DataContext() { }

        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
        
             public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Buyers { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<OrderTransaction> OrderTransactions { get; set; }
        public DbSet<Book> Books{ get; set; }
        public DbSet<Seller> Sellers { get; set; }
        public DbSet<StorageTransaction> StorageTransactions { get; set; }
        public static readonly ILoggerFactory MyLoggerFactory = LoggerFactory.Create(builder =>
        {
            Console.WriteLine(builder
            .AddFilter((category, level) => category == DbLoggerCategory.Database.Name
            && level == LogLevel.Information));
        });
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLoggerFactory(MyLoggerFactory)
                .EnableSensitiveDataLogging()
               .UseNpgsql("Host=localhost;Port=5432;Database=BookStore;Username=postgres;Password=123456789");
        }
    }
}

