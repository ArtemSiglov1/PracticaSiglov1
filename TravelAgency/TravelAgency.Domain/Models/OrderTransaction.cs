using System;

namespace TravelAgency.Domain.Models
{
    public class OrderTransaction
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public int Id { get; set; }
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
        public int OrderId { get; set; }
        /// <summary>
        /// заказ
        /// </summary>
        public Order Order { get; set; }
    }
}