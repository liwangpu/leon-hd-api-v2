using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Basic.Export;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Localization;

namespace App.MoreJee.API.Application.Queries.ProductPermissionGroups
{
    public class ProdutPermissionGroupOwnOrganQueryHandler : IRequestHandler<ProdutPermissionGroupOwnOrganQuery, List<ProdutPermissionGroupOwnOrganQueryDTO>>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly OrganizationService organizationService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        #region ctor
        public ProdutPermissionGroupOwnOrganQueryHandler(IProductPermissionGroupRepository productPermissionGroupRepository, OrganizationService organizationService, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.organizationService = organizationService;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<List<ProdutPermissionGroupOwnOrganQueryDTO>> Handle(ProdutPermissionGroupOwnOrganQuery request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.ProductPermissionGroupId);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductPermissionGroup", request.ProductPermissionGroupId]);

            await productPermissionGroupRepository.LoadOwnOrganItemsAsync(data);

            var list = new List<ProdutPermissionGroupOwnOrganQueryDTO>();
            if (data.OwnOrganItems.Count == 0) return list;

            var organIdArr = data.OwnOrganItems.Select(x => x.OrganizationId).ToList();
            var organDtos = await organizationService.GetBriefByIds(string.Join(",", organIdArr));

            if (!string.IsNullOrWhiteSpace(request.Search))
            {
                for (var idx = organDtos.Count - 1; idx >= 0; idx--)
                {
                    var it = organDtos[idx];
                    if (!it.Name.ToLower().Contains(request.Search.ToLower()))
                        organDtos.RemoveAt(idx);
                }
            }


            foreach (var oit in data.OwnOrganItems)
            {
                var organ = organDtos.FirstOrDefault(x => x.Id == oit.OrganizationId);
                if (organ == null) continue;
                list.Add(ProdutPermissionGroupOwnOrganQueryDTO.From(oit.Id, organ.Name, organ.Description));
            }

            return list;
        }
        #endregion
    }
}
