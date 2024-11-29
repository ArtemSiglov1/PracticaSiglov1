using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Interface.Models.RegAndLog;

namespace TravelAgency.Service.Validators
{
   public class UserValidator : AbstractValidator<RegAndLogModel>
    {
       
        public UserValidator(RegAndLogModel loginRegistr) {
            if (loginRegistr.LoginUser != null) {
                RuleFor(login => login.LoginUser.Password).NotNull().NotEmpty().WithMessage("Пароль обязателен")
                    .MinimumLength(6).WithMessage("Минимальная длинна 6 символов");
                RuleFor(login => login.LoginUser.Email).NotNull().NotEmpty().WithMessage("Email обязателен")
                    .EmailAddress().WithMessage("Неверный формат Email");
                    }
            if (loginRegistr.RegistrationUser != null)
            {
                RuleFor(login => login.RegistrationUser.Password).NotNull().NotEmpty().WithMessage("Пароль обязателен")
    .MinimumLength(6).WithMessage("Минимальная длинна 6 символов");
                RuleFor(login => login.RegistrationUser.Email).NotNull().NotEmpty().WithMessage("Email обязателен")
                    .EmailAddress().WithMessage("Неверный формат Email");
                RuleFor(login => login.RegistrationUser.Login).NotNull().NotEmpty().WithMessage("Логин обязателен")
                    .MinimumLength(3).MaximumLength(20).WithMessage("Логин должен иметь длину от 3 до 20 символов");

            }
        }
        
    }
}
