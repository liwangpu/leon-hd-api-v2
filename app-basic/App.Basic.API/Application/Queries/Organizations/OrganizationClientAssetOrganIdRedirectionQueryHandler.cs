using App.Base.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationClientAssetOrganIdRedirectionQueryHandler : IRequestHandler<OrganizationClientAssetOrganIdRedirectionQuery, string>
    {
        private readonly IIdentityService identityService;
        private readonly IOrganizationRepository organizationRepository;

        #region ctor
        public OrganizationClientAssetOrganIdRedirectionQueryHandler(IIdentityService identityService, IOrganizationRepository organizationRepository)
        {
            this.identityService = identityService;
            this.organizationRepository = organizationRepository;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(OrganizationClientAssetOrganIdRedirectionQuery request, CancellationToken cancellationToken)
        {
            var currentOrganId = identityService.GetOrganizationId();
            var currentOrgan = await organizationRepository.FindAsync(currentOrganId);
            //品牌商
            if (currentOrgan.OrganizationTypeId == OrganizationType.Brand.Id)
                return currentOrgan.Id;
            //代理商或者供应商
            if (currentOrgan.OrganizationTypeId == OrganizationType.Partner.Id || currentOrgan.OrganizationTypeId == OrganizationType.Supplier.Id)
            {
                var tid = await organizationRepository.GetTopTreeOrganization(currentOrganId).Select(x => x.Id).FirstAsync();
                return tid;
            }

            return string.Empty;
        } 
        #endregion
    }
}
