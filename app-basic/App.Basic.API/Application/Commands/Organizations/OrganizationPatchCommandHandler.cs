using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.Basic.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationPatchCommandHandler : IRequestHandler<OrganizationPatchCommand>
    {
        private readonly IOrganizationRepository organRepository;
        private readonly IIdentityService identityService;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public OrganizationPatchCommandHandler(IOrganizationRepository organRepository, IIdentityService identityService, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.organRepository = organRepository;
            this.identityService = identityService;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        public async Task<Unit> Handle(OrganizationPatchCommand request, CancellationToken cancellationToken)
        {
            var organ = await organRepository.FindAsync(request.Id);
            if (organ == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Organization", request.Id]);

            var canOperated = organRepository.GetManagedOrganization(identityService.GetUserId()).Any(x => x.Id == request.Id);
            if (!canOperated)
                throw new HttpForbiddenException(commonLocalizer["OperateForbidden"]);

            mapper.Map(organ, request);
            request.ApplyPatch();
            organ.UpdateBasicInfo(request.Name, request.Description, identityService.GetUserId());
            organRepository.Update(organ);
            await organRepository.UnitOfWork.SaveEntitiesAsync();
            return Unit.Value;
        }
    }
}
