using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;

namespace TravelAgency.Interface.Models
{
    public class ShopGetListRequest
    {
        public ShopGetListRequest() { }

        public ShopGetListRequest(string search, PageRequest page)
        {
            Search = search;
            Page = page;
        }

        public string Search {  get; set; }
        public PageRequest Page { get; set; }
    }
}
