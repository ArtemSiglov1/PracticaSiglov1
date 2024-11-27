using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelAgency.Domain.ViewModels
{
   public class RegisterViewModel
    {
        public RegisterViewModel(string login, string email, string password, string confirmPassword)
        {
            Login = login;
            Email = email;
            Password = password;
            PasswordConfirm = confirmPassword;
        }


        [Required(ErrorMessage = "Укажите имя 3-20 символов")]
        [MaxLength(20, ErrorMessage = "Имя должно иметь длину меньше 20 символов")]
        [MinLength(3, ErrorMessage = "Имя должно иметь длину более 3 символов")]
        public string Login { get; set; }

        [EmailAddress(ErrorMessage = "Некорректный адрес эдектронной почты")]
        [Required(ErrorMessage = "Укажите почту")]
        public string Email { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Введите пароль")]
        [MinLength(6, ErrorMessage = "Пароль должен иметь длину больше 6 символов")]
        public string Password { get; set; }

        [DataType(DataType.Password)]
        [Required(ErrorMessage = "Подтвердить пароль")]
        [Compare("Password", ErrorMessage = "Пароли не совпадают")]
        public string PasswordConfirm { get; set; }
        public RegisterViewModel() { }
    }
}
