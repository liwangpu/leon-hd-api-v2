using App.Base.API;
using App.Base.API.Infrastructure.ActionResults;
using App.Base.API.Infrastructure.Extensions;
using App.Base.API.Infrastructure.Services;
using App.Basic.API.Infrastructure.Services;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationBatchDeleteCommandHandler : IRequestHandler<OrganizationBatchDeleteCommand, ObjectResult>
    {
        private readonly IOrganizationRepository organRepository;
        private readonly IUriService uriService;
        private readonly IUserManagedOrganizationService userManagedOrganizationService;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        public OrganizationBatchDeleteCommandHandler(IOrganizationRepository organRepository, IUriService uriService, IUserManagedOrganizationService userManagedOrganizationService, IIdentityService identityService, IStringLocalizer<CommonTranslation> localizer)
        {
            this.organRepository = organRepository;
            this.uriService = uriService;
            this.userManagedOrganizationService = userManagedOrganizationService;
            this.identityService = identityService;
            this.localizer = localizer;
        }

        public async Task<ObjectResult> Handle(OrganizationBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new MultiStatusObjectResult();
            var operatorId = identityService.GetUserId();
            var resourcePartUri = uriService.GetUriWithoutQuery().URIUpperLevel();
            var organIdArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0, len = organIdArr.Count(); i < len; i++)
            {
                var organId = organIdArr[i];
                var uri = $"{resourcePartUri}/{organId}";
               
                var organ = await organRepository.FindAsync(organId);
                if (organ == null)
                {
                    result.AddResult(uri, 404, "");
                    continue;
                }

                var query = await userManagedOrganizationService.GetManagedOrganizations(operatorId);
                var canOperat = await query.AnyAsync(x => x.Id == organId);
                if (!canOperat)
                {
                    result.AddResult(uri, 403, localizer["OperateForbidden"]);
                    continue;
                }


                await organRepository.DeleteAsync(organId, operatorId);
                result.AddResult(uri, 200, "");
            }

            return result.Transfer();
        }
    }
}
