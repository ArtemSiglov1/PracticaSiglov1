using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestUsers.Interface.Models;
using TravelAgency.Domain.Models;
using TravelAgency.Interface.Models.ClientServiceModel;

namespace TravelAgency.Interface
{
    public interface IClientService
    {
        /// <summary>
        /// создание заказов
        /// </summary>
        /// <param name="buyerId">идентиф покуп</param>
        /// <param name="sellerId">идентиф продавца</param>
        /// <param name="items">покупки</param>
        /// <returns></returns>
        public Task<BaseResponse< Order?>> CreateOrders(CreateOrderModel model);
        public Task<BaseResponse<Order?>> GetOrder(Guid id);
    }
}
