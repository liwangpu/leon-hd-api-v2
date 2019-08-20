using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.OMS.Domain.AggregateModels.CustomerAggregate;
using App.OMS.Infrastructure.Specifications.CustomerSpecifications;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.OMS.API.Application.Queries.Customers
{
    public class CustomerPagingQueryHandler : IRequestHandler<CustomerPagingQuery, PagingQueryResult<CustomerPagingQueryDTO>>
    {
        private readonly ICustomerRepository customerRepository;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        #region ctor
        public CustomerPagingQueryHandler(ICustomerRepository customerRepository, IMapper mapper, IIdentityService identityService)
        {
            this.customerRepository = customerRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<CustomerPagingQueryDTO>> Handle(CustomerPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<CustomerPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new CustomerPagingSpecification(identityService.GetOrganizationId(), request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search);
            result.Total = await customerRepository.Get(specification).CountAsync();
            result.Data = await customerRepository.Paging(specification).Select(x => mapper.Map<CustomerPagingQueryDTO>(x)).ToListAsync();
            return result;
        } 
        #endregion
    }
}
