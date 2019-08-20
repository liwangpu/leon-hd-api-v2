using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Queries.Customers
{
    public class CustomerIdentityQueryHandler : IRequestHandler<CustomerIdentityQuery, CustomerIdentityQueryDTO>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        #region ctor
        public CustomerIdentityQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IStringLocalizer<CommonTranslation> localizer)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.localizer = localizer;
        }
        #endregion

        #region Handle
        public async Task<CustomerIdentityQueryDTO> Handle(CustomerIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await customerRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "Customer", request.Id]);
            return CustomerIdentityQueryDTO.From(data);
        }
        #endregion
    }
}
