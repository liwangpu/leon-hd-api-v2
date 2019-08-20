using App.Base.API.Infrastructure.Exceptions;
using App.OMS.API.Application.Queries.Orders;
using FluentValidation;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Validations.Orders
{
    public class OrderIdentityQueryValidator : AbstractValidator<OrderIdentityQuery>
    {
        public OrderIdentityQueryValidator()
        {
            RuleFor(x => x.Id).MustAsync(async (id, token) => await ExistEntity(id));
        }

        public async Task<bool> ExistEntity(string id)
        {

            //throw new HttpResourceNotFoundException("sdfsdfdf");

            return true;
        }
    }
}
