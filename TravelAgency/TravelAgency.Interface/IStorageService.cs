using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;
using TravelAgency.Interface.Models;

namespace TravelAgency.Interface
{
    public interface IStorageService
    {
        /// <summary>
        /// метод для получения всех продуктов
        /// </summary>
        /// <param name="orders">заказ</param>
        /// <param name="shopId">идентиф магаза</param>
        /// <returns>рандомный товар из бд</returns>
        public Task GetProduct(List<OrderItem> orders, Guid shopId);
        public Task<List<Book>> GetProduct();
        public Task<Seller?> GetSeller(Guid id);
        public Task<List<Seller>> GetSellers(SellerListRequest request);
        /// <summary>
        /// создать магазин
        /// </summary>
        /// <param name="shop">магазин</param>
        /// <returns>задачу</returns>
        public Task CreateShop(Shop shop);

        /// <summary>
        /// выводит все магазины содержащиеся в бд
        /// </summary>
        /// <returns>лист магазинов</returns>
        public Task<List<Shop>> GetShops(ShopGetListRequest request);
        public Task<Shop?> GetShop(Guid shopId);
        public Task<List<StorageTransaction>> GetStorageTransaction(Guid shopId);
        public Task<List<OrderTransaction>> GetOrderTransactions(Guid sellerId);

        /// <summary>
        /// добавление продавца
        /// </summary>
        /// <param name="shopId">идентиф магаза</param>
        /// <param name="seller">продавец</param>
        /// <returns>задачу</returns>
        public Task AddSeller(Guid shopId, Seller seller);
        /// <summary>
        /// создание продукта
        /// </summary>
        /// <param name="product">продукт</param>
        /// <returns>задача</returns>
        public Task CreateProduct(Book product);
        /// <summary>
        /// добавление покупателей
        /// </summary>
        /// <param name="buyers">лист покупателей</param>
        /// <returns>задача</returns>
        public Task AddBuyer(List<User> buyers);
        /// <summary>
        /// доставка продуктов
        /// </summary>
        /// <param name="shopId">идентиф магазина</param>
        /// <param name="productId">идентиф продукта</param>
        /// <param name="count">количество</param>
        /// <returns>задача</returns>
        public Task DeliverGoods(Guid shopId, Guid productId, int count);


    }
}
