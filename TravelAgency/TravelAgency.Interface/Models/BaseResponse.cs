using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.Enums;

namespace TestUsers.Interface.Models
{
    /// <summary>
    /// базовый ответ
    /// </summary>
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; }
        public StatucCode StatusCode { get; set; }
        public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        T Data { get; set; }
    }
}
