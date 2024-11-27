using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestUsers.Interface.Models
{
    /// <summary>
    /// базовый ответ
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// успешно
        /// </summary>
       public bool IsSuccess { get; set; }
  /// <summary>
  /// соо об ошибке
  /// </summary>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// конструктор по умолчанию
        /// </summary>
        public BaseResponse() { }
        public BaseResponse(bool isSuccess)
        {
            IsSuccess = isSuccess;
        }
        /// <summary>
        /// с параметрами
        /// </summary>
        /// <param name="isSuccess">успех</param>
        /// <param name="errorMessage">соо об ошибке </param>
        public BaseResponse(bool isSuccess, string errorMessage):this(isSuccess)
        {
            ErrorMessage = errorMessage;
        }
    }
}
