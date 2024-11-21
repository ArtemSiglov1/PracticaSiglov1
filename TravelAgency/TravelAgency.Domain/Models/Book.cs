using System;
using System.Collections.Generic;

namespace TravelAgency.Domain.Models
{
    public class Book
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// имя продукта
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// список транзакций продукта привоз\отвоз
        /// </summary>
        public List<StorageTransaction> Transactions { get; set; }
        /// <summary>
        /// список покупок в заказе
        /// </summary>
        public List<OrderItem> Items { get; set; }
        public string PathImg { get; set; }
        public decimal Price { get; set; }
    }
}