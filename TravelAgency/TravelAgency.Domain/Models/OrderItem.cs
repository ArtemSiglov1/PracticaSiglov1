using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class OrderItem
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// идентиф заказа
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// заказ
        /// </summary>
        public Order Order { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Book Book{ get; set; }
        /// <summary>
        /// идентиф продукта
        /// </summary>
        public Guid BookId { get; set; }
        /// <summary>
        /// стоимость 
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// количество заказанного товара
        /// </summary>
        public int Count { get; set; }
    }
}
