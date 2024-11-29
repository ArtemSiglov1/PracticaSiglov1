using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Domain.ViewModels;

namespace TravelAgency.Domain.Validators
{
   public class UserValidator : AbstractValidator<LoginRegistrView>
    {
       
        public UserValidator(LoginRegistrView loginRegistr) {
            if (loginRegistr.Login != null) {
                RuleFor(login => login.Login.Password).NotNull().NotEmpty().WithMessage("Пароль обязателен")
                    .MinimumLength(6).WithMessage("Минимальная длинна 6 символов");
                RuleFor(login => login.Login.Email).NotNull().NotEmpty().WithMessage("Email обязателен")
                    .EmailAddress().WithMessage("Неверный формат Email");
                    }
            if (loginRegistr.Register != null)
            {
                RuleFor(login => login.Register.Password).NotNull().NotEmpty().WithMessage("Пароль обязателен")
    .MinimumLength(6).WithMessage("Минимальная длинна 6 символов");
                RuleFor(login => login.Register.Email).NotNull().NotEmpty().WithMessage("Email обязателен")
                    .EmailAddress().WithMessage("Неверный формат Email");
                RuleFor(login => login.Register.Login).NotNull().NotEmpty().WithMessage("Логин обязателен")
                    .MinimumLength(3).MaximumLength(20).WithMessage("Логин должен иметь длину от 3 до 20 символов");

            }
        }
        
    }
}
