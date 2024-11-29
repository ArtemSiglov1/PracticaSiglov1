using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.Models
{
    public class Seller
    {
        /// <summary>
        /// идентиф
        /// </summary>
        public Guid Id { get; set; }
        /// <summary>
        /// имя продавца
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// идентиф магазина
        /// </summary>
        public Guid ShopId { get; set; }
        /// <summary>
        /// магазин
        /// </summary>
        public Shop Shop { get; set; }
        /// <summary>
        /// список заказов которые получил этот продавец
        /// </summary>
        public List<Order> Orders { get; set; }
    }
}
