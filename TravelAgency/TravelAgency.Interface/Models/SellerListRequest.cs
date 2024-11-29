using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;

namespace TravelAgency.Interface.Models
{
    public class SellerListRequest
    {
        public SellerListRequest() { }

        public SellerListRequest(string search, PageRequest page, Guid shopId)
        {
            Search = search;
            Page = page;
            ShopId = shopId;
        }

        public string Search {  get; set; }
        public PageRequest Page { get; set; }
        public Guid ShopId { get; set; }
    }
}
