using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Order
    {
        public Guid Id { get; set; }
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// список заказанных предметов
        /// </summary>
        public List<OrderItem> Items { get; set; }
        /// <summary>
        /// идентиф покупателя
        /// </summary>
        public Guid BuyerId { get; set; }
        /// <summary>
        /// покупатель для связи 2 таблиц
        /// </summary>
        public User Buyer { get; set; }
        /// <summary>
        /// идентиф продавца
        /// </summary>
        public Guid SellerId { get; set; }
        /// <summary>
        /// продавец
        /// </summary>
        public Seller Seller { get; set; }

        /// <summary>
        /// объект для слежения за финансами
        /// </summary>
        public List<OrderTransaction> OrderTransaction { get; set; }
    }
}
