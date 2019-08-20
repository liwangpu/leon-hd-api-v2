using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.OrganizationTypes
{
    public class OrganizationTypeIdentityQueryHandler : IRequestHandler<OrganizationTypeIdentityQuery, OrganizationTypeIdentityQueryDTO>
    {
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IStringLocalizer<AppBasicTranslation> appLocalizer;


        #region ctor
        public OrganizationTypeIdentityQueryHandler(IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<AppBasicTranslation> appLocalizer)
        {
            this.commonLocalizer = commonLocalizer;
            this.appLocalizer = appLocalizer;
        }
        #endregion

        #region Handle
        public async Task<OrganizationTypeIdentityQueryDTO> Handle(OrganizationTypeIdentityQuery request, CancellationToken cancellationToken)
        {
            var organType = Enumeration.FromValue<OrganizationType>(request.Id);
            if (organType == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "OrganizationType", request.Id]);
            var dto = new OrganizationTypeIdentityQueryDTO();
            dto.Id = organType.Id;
            dto.Name = appLocalizer[organType.Name];
            dto.Description = appLocalizer[organType.Description];
            return await Task.FromResult(dto);
        }
        #endregion
    }
}
