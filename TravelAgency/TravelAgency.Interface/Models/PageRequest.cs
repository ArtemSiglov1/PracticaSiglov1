using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Interface.Models
{
    /// <summary>
    /// страница запроса
    /// </summary>
    public class PageRequest
    {
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="page">страница</param>
        /// <param name="pageSize">размер страницы</param>
        public PageRequest(int page, int pageSize)
        {
            Page = page;
            PageSize = pageSize;
        }

    /// <summary>
    /// по умолчанию
    /// </summary>
        public PageRequest() { }
        /// <summary>
        /// страница
        /// </summary>
        public int Page { get; set; } //номер страницы от 1
        /// <summary>
        /// размер страниц
        /// </summary>
 public int PageSize { get; set; } // количество записей на странице


    }
}
