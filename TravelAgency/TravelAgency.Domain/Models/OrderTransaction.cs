using System;

namespace TravelAgency.Domain.Models
{
    public class OrderTransaction
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// дата продажи товара
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// стоимость проданного товара
        /// </summary>
        public decimal Cost { get; set; }
        /// <summary>
        /// идентиф заказа
        /// </summary>
        public Guid OrderId { get; set; }
        /// <summary>
        /// заказ
        /// </summary>
        public Order Order { get; set; }
    }
}