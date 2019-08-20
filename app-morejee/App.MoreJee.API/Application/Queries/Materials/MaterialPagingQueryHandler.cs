using App.Base.API.Application.Queries;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.Materials;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Materials
{
    public class MaterialPagingQueryHandler : IRequestHandler<MaterialPagingQuery, PagingQueryResult<MaterialPagingQueryDTO>>
    {
        private readonly IMaterialRepository materialRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public MaterialPagingQueryHandler(IMaterialRepository materialRepository, ICategoryRepository categoryRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.materialRepository = materialRepository;
            this.categoryRepository = categoryRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<MaterialPagingQueryDTO>> Handle(MaterialPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<MaterialPagingQueryDTO>();
            request.CheckPagingParam();

            if (!request.UnClassified)
                request.CategoryId = await categoryRepository.GetAllSubCategoryIds(request.CategoryId);

            var clientOrganId = await clientAssetPermissionControlService.ClientAssetOrganIdRedirection();

            var specification = new MaterialPagingSpecification(clientOrganId,request.Page, request.PageSize, request.OrderBy, request.Desc, request.Search, request.CategoryId, request.UnClassified);
            var datas = await materialRepository.Paging(specification).Select(x => new { x.Id, x.Name, x.Description, x.Icon, x.CategoryId, x.CreatedTime, x.ModifiedTime }).ToListAsync();
            var dtos = datas.Select(x => MaterialPagingQueryDTO.From(x.Id, x.Name, x.Description, x.Icon, x.CategoryId, x.CreatedTime, x.ModifiedTime)).ToList();
            for (var idx = dtos.Count - 1; idx >= 0; idx--)
            {
                var it = dtos[idx];
                it.CategoryName = await categoryRepository.GetCategoryName(it.CategoryId);
            }
            result.Total = await materialRepository.Get(specification).CountAsync();
            result.Data = dtos;
            return result;
        }
        #endregion
    }
}
