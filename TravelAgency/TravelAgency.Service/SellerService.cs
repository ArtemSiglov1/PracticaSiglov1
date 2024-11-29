using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.DAL;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Models;
using TravelAgency.Interface;

namespace TravelAgency.Service
{
    public class SellerService: ISellerService
    {
        /// <summary>
        /// 
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbContextOptions"></param>

        public SellerService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<List<OrderItem>> CheckProduct(Order order, Guid shopId)
        {
            await using var db = new DataContext(_dbContextOptions);

            var productIds = order.Items.Select(x => x.BookId).ToArray();


            var productCount = await db.StorageTransactions
                 .Where(x => x.ShopId == shopId && productIds.Contains(x.BookId))
                 .GroupBy(x => x.BookId)
                 .Select(g => new
                 {
                     ProductId = g.Key,
                     Count = g.Sum(x => x.TransactionType == StorageTransactionType.Take ? x.Count : -x.Count)
                 })
                 .ToArrayAsync();


            var availableProduct = productCount.Where(p => order.Items.Any(x => x.BookId == p.ProductId)).FirstOrDefault();
            if (availableProduct == null)
                if (order.Items.Any(x => x.Count > availableProduct.Count))
                    return new List<OrderItem> { new() { BookId = availableProduct.ProductId } };
                
            
            return new List<OrderItem> { };
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <param name="shopId"></param>
        /// <returns></returns>
        public async Task<bool> ProcessOrder(Order order, Guid shopId)
        {

            await using var db = new DataContext(_dbContextOptions);
            await using var transact = await db.Database.BeginTransactionAsync();
            var product = await CheckProduct(order, shopId);
            var productIds = order.Items.Select(x => x.BookId).ToArray();
            var products = await db.Books.Where(x => productIds.Contains(x.Id)).ToListAsync();
            if (product.Count<=0)
            {
                return false;
            }
           var storages = new List<StorageTransaction>();
            var orders = new List<OrderTransaction>();
            try
            {
                foreach (var item in order.Items)
                {
                    storages.Add(new StorageTransaction
                    {
                        ShopId = shopId,
                        BookId = item.BookId,
                        Count = item.Count, // Списание товара
                        TransactionType = StorageTransactionType.Ship,
                        DateCreate = DateTime.UtcNow
                    });
                    foreach (var elem in products)
                    {
                        orders.Add(new OrderTransaction
                        {
                            Cost = (decimal)(item.Count * elem.Price),
                            DateCreate = DateTime.UtcNow,
                            OrderId = item.OrderId,
                        });
                    }
                }

                await db.StorageTransactions.BulkInsertAsync(storages);
                await db.OrderTransactions.BulkInsertAsync(orders);
                await db.SaveChangesAsync();
                await transact.CommitAsync();
                return true;
            }
            catch
            {
                await transact.RollbackAsync();
                return false;
            }
        }
        }
    }
