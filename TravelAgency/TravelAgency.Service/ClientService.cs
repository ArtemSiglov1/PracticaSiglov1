using FluentValidation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.DAL;
using TravelAgency.Domain.Enums;
using TravelAgency.Domain.Models;
using TravelAgency.Interface;
using TravelAgency.Interface.Models.ClientServiceModel;
using TravelAgency.Service.Validators.ClientValidator;

namespace TravelAgency.Service
{
    public class ClientService: IClientService
    {
        /// <summary>
        /// настройки для работы с бд
        /// </summary>
        private DbContextOptions<DataContext> _dbContextOptions;


        /// <summary>
        /// конструктор
        /// </summary>
        /// <param name="dbContextOptions">настройки для работы с бд</param>
        public ClientService(DbContextOptions<DataContext> dbContextOptions)
        {
            _dbContextOptions = dbContextOptions;
        }
        /// <summary>
        /// лист продуктов
        /// </summary>
        public List<Book>? Products { get; set; }
        /// <summary>
        /// метод для создания заказов
        /// </summary>
        /// <param name="buyerId">идентиф покупателя</param>
        /// <param name="sellerId">идентиф продавца</param>
        /// <param name="items">список покупок</param>
        /// <returns>заказ</returns>
        public async Task<BaseResponse<Order?>> CreateOrders(CreateOrderModel model)
        {
            try {
                var valid = new CreateOrderModelValidator();
                await valid.ValidateAndThrowAsync(model);
                await using var db = new DataContext(_dbContextOptions);
                var buyer = await db.Buyers.AnyAsync(x => x.Id == model.BuyerId);
                if (!buyer)
                    return new BaseResponse<Order>() { StatusCode = StatucCode.BadRequest, Description = "Покупатель с данным не найден" };
                
                var seller = await db.Sellers.AnyAsync(db => db.Id == model.SellerId);
                if (!seller)
                    return new BaseResponse<Order>() { StatusCode = StatucCode.BadRequest, Description = "Продовец с данным айди не найден" };
                
                var order = new Order { BuyerId = model.BuyerId, SellerId = model.SellerId, Items = model.Items, DateCreate = DateTime.UtcNow };
                await db.Orders.AddAsync(order);
                await db.SaveChangesAsync();
                return new BaseResponse<Order?>
                {
                    Data = order,
                    StatusCode = StatucCode.OK,
                };
            }
            catch (ValidationException exception)
            {
                var errorMessage = string.Join(";", exception.Errors.Select(e => e.ErrorMessage));
                return new BaseResponse<Order>()
                {
                    Description = errorMessage,
                    StatusCode = StatucCode.BadRequest
                };
            }
            catch (Exception ex)
            {
                return new BaseResponse<Order>()
                {
                    Description = ex.Message,
                    StatusCode = StatucCode.InternalServerError
                };
            }
        }
        public async Task<BaseResponse< Order?>> GetOrder(Guid id)
        {
            await using var db = new DataContext(_dbContextOptions);
            var order = await db.Orders.Where(x => x.Id == id).Select(x => new Order
            {
                Id = x.Id,
                DateCreate = x.DateCreate,
                Buyer = x.Buyer,
                Seller = x.Seller,
                BuyerId = x.BuyerId,
                Items = x.Items.Select(x => new OrderItem()
                {
                    Id = x.Id,
                    Cost = x.Cost,
                    Count = x.Count,
                    Order = x.Order,
                    OrderId = x.OrderId,
                    Book = x.Book,
                    BookId = x.BookId

                }).ToList(),
                SellerId = x.SellerId,
                OrderTransaction = x.OrderTransaction
            }).FirstOrDefaultAsync();
            if (order == null)
                return new BaseResponse<Order?> { Description = "Заказ с данным айди не найден", StatusCode = StatucCode.BadRequest };
            return new BaseResponse<Order?>{Data= order, StatusCode= StatucCode.OK};
        }
    }
}
