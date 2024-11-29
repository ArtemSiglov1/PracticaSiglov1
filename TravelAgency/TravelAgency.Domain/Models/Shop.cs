using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Shop
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// название магаза
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// список продавцов работающих в магазе
        /// </summary>
        public List<Seller> Sellers { get; set; }
        /// <summary>
        /// список транзакций на складе привоз\отвоз
        /// </summary>
        public List<StorageTransaction> Transactions { get; set; }
    }
}
