using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupPatchCommandHandler : IRequestHandler<ProductPermissionGroupPatchCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public ProductPermissionGroupPatchCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupPatchCommand request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductPermissionGroup", request.Id]);
            mapper.Map(data, request);
            request.ApplyPatch();
            data.UpdateBasicInfo(request.Name, request.Description);
            await productPermissionGroupRepository.UpdateAsync(data);
            return Unit.Value;
        } 
        #endregion
    }
}
