﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Interface
{
    public interface ISellerService
    {
        /// <summary>
        /// проверка хватает ли нужного товара на складе если не хватает оформялет транзакцию на привоз
        /// </summary>
        /// <param name="order">заказ</param>
        /// <param name="shopId">идентиф магаза</param>
        /// <param name="sellerId">идентиф продавца</param>
        /// <returns></returns>
        public Task<bool> ProcessOrder(Order order, Guid shopId);
        /// <summary>
        /// проверяет каких товаров не хватило для выполнения заказов и возвращает их для заказа у поставщика
        /// </summary>
        /// <param name="order">заказ</param>
        /// <param name="shopId">идентиф магазина</param>
        /// <returns>товары которых не хватило для заказа</returns>
        public Task<List<OrderItem>> CheckProduct(Order order, Guid shopId);

    }
}
