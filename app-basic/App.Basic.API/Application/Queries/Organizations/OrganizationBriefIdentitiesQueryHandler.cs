using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.OrganizationSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationBriefIdentitiesQueryHandler : IRequestHandler<OrganizationBriefIdentitiesQuery, List<OrganizationBriefIdentitiesQueryDTO>>
    {
        private readonly IOrganizationRepository organRepository;

        #region ctor
        public OrganizationBriefIdentitiesQueryHandler(IOrganizationRepository organRepository)
        {
            this.organRepository = organRepository;
        }
        #endregion

        #region Handle
        public async Task<List<OrganizationBriefIdentitiesQueryDTO>> Handle(OrganizationBriefIdentitiesQuery request, CancellationToken cancellationToken)
        {
            var specification = new GetBriefOrganizationByIdsSpecification(request.Ids);
            return await organRepository.Get(specification).Select(x => OrganizationBriefIdentitiesQueryDTO.From(x.Id, x.Name, x.Description)).ToListAsync();
        }
        #endregion
    }
}
