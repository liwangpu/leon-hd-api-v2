using App.Base.API.Infrastructure.Services;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Commands.Customers
{
    public class CustomerCreateCommandHandler : IRequestHandler<CustomerCreateCommand, string>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public CustomerCreateCommandHandler(ICustomerRepository customerRepository, IIdentityService identityService)
        {
            this.customerRepository = customerRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(CustomerCreateCommand request, CancellationToken cancellationToken)
        {
            var data = new Customer(request.Name, request.Description, request.Company, request.Phone, request.Mail, request.Address, identityService.GetOrganizationId(), identityService.GetUserId());
            await customerRepository.AddAsync(data);

            return data.Id;
        } 
        #endregion
    }
}
