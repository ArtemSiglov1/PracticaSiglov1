using System;
using TravelAgency.Domain.Enums;

namespace TravelAgency.Domain.Models
{
    public class StorageTransaction
    { 
        /// <summary>
      /// идентиф
      /// </summary>
        public int Id { get; set; }
       
        /// <summary>
        /// идентиф продукта
        /// </summary>
        public int BookId { get; set; }
        /// <summary>
        /// продукт
        /// </summary>
        public Book Book { get; set; }
        /// <summary>
        /// количество
        /// </summary>
        public double Count { get; set; }
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