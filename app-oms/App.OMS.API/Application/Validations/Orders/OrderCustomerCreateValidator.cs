using App.Base.API;
using App.OMS.API.Application.Commands.Orders;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using App.OMS.Domain.AggregateModels.OrderAggregate;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Validations.Orders
{
    public class OrderCustomerCreateValidator : AbstractValidator<OrderCustomerCreateCommand>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IOrderRepository orderRepository;

        public OrderCustomerCreateValidator(IStringLocalizer<CommonTranslation> commonLocalizer, ICustomerRepository customerRepository, IOrderRepository orderRepository)
        {
            this.customerRepository = customerRepository;
            this.orderRepository = orderRepository;
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.Phone).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Phone"]);
            RuleFor(cmd => cmd.Address).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Address"]);
            RuleFor(cmd => cmd.CustomerId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "CustomerId"]);
            RuleFor(cmd => cmd.OrderId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "OrderId"]);
            RuleFor(x => x.CustomerId).MustAsync(async (id, token) => await ExistCustomer(id)).WithMessage(x => commonLocalizer["HttpRespond.BadRequest.1", "Customer", x.CustomerId]);
            RuleFor(x => x.OrderId).MustAsync(async (id, token) => await ExistOrder(id)).WithMessage(x => commonLocalizer["HttpRespond.BadRequest.1", "Order", x.OrderId]);


        }

        public async Task<bool> ExistCustomer(string id)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(id)) return true;

            var data = await customerRepository.FindAsync(id);
            return data != null;
        }

        public async Task<bool> ExistOrder(string id)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(id)) return true;

            var data = await orderRepository.FindAsync(id);
            return data != null;
        }
    }
}
