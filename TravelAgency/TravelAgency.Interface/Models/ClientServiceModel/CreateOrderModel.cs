using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Models;

namespace TravelAgency.Interface.Models.ClientServiceModel
{
    public class CreateOrderModel
    {
        public Guid BuyerId { get; set; }
        public Guid SellerId { get; set; }
        public List<OrderItem> Items {  get; set; }
    }
}
