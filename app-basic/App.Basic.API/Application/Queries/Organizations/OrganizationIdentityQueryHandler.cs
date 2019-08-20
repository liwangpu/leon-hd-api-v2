using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Basic.Domain.AggregateModels.UserAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationIdentityQueryHandler : IRequestHandler<OrganizationIdentityQuery, OrganizationIdentityQueryDTO>
    {
        private readonly IOrganizationRepository organRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IMapper mapper;

        #region ctor
        public OrganizationIdentityQueryHandler(IOrganizationRepository organRepository, IStringLocalizer<CommonTranslation> localizer, IMapper mapper)
        {
            this.organRepository = organRepository;
            this.localizer = localizer;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<OrganizationIdentityQueryDTO> Handle(OrganizationIdentityQuery request, CancellationToken cancellationToken)
        {
            var organ = await organRepository.FindAsync(request.Id);
            if (organ == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "Organization", request.Id]);
            return mapper.Map<OrganizationIdentityQueryDTO>(organ);
        } 
        #endregion
    }
}
