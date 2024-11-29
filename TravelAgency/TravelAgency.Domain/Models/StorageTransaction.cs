using System;
using TravelAgency.Domain.Enums;

namespace TravelAgency.Domain.Models
{
    public class StorageTransaction
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// идентиф магазина
        /// </summary>
        public Guid ShopId { get; set; }
        /// <summary>
        /// магазин
        /// </summary>
        public Shop Shop { get; set; }
        /// <summary>
        /// идентиф продукта
        /// </summary>
        public Guid BookId{ get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// количество
        /// </summary>
        public int Count { get; set; }
        /// <summary>
        /// дата транзакции
        /// </summary>
        public DateTime DateCreate { get; set; }
        /// <summary>
        /// тип транзакции привоз\отвоз
        /// </summary>
        public StorageTransactionType TransactionType { get; set; }

    }
}