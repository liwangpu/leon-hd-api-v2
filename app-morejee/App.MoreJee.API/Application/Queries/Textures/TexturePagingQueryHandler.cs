using App.Base.API.Application.Queries;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using App.MoreJee.Infrastructure.Specifications.TextureSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Textures
{
    public class TexturePagingQueryHandler : IRequestHandler<TexturePagingQuery, PagingQueryResult<TexturePagingQueryDTO>>
    {
        private readonly ITextureRepository textureRepository;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public TexturePagingQueryHandler(ITextureRepository textureRepository, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.textureRepository = textureRepository;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<TexturePagingQueryDTO>> Handle(TexturePagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<TexturePagingQueryDTO>();
            request.CheckPagingParam();

            var clientOrganId = await clientAssetPermissionControlService.ClientAssetOrganIdRedirection();
            var specification = new TexturePagingSpecification(clientOrganId,request.Page, request.PageSize, request.Search, request.OrderBy, request.Desc);
            var datas = await textureRepository.Paging(specification).Select(x => new { x.Id, x.Name, x.CreatedTime, x.ModifiedTime }).ToListAsync();
            result.Total = await textureRepository.Get(specification).CountAsync();
            result.Data = datas.Select(x => TexturePagingQueryDTO.From(x.Id, x.Name, x.CreatedTime, x.ModifiedTime)).ToList();
            return result;
        }
        #endregion
    }
}
