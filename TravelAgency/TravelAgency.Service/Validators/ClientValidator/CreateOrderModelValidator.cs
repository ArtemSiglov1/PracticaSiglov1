using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TravelAgency.Interface.Models;
using TravelAgency.Interface.Models.ClientServiceModel;
namespace TravelAgency.Service.Validators.ClientValidator
{
    public class CreateOrderModelValidator:AbstractValidator<CreateOrderModel>
    {
        public CreateOrderModelValidator() { 
        RuleFor(x=>x.Items).NotEmpty().WithMessage("В заказе должен быть хотябы один продукт");
            RuleFor(x => x.SellerId).NotNull().WithMessage("Ид продавца не может быть пустым");
            RuleFor(x => x.BuyerId).NotNull().WithMessage("Ид покупателя не может быть пустым");
        }
    }
}
