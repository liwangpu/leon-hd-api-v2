using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupBatchDeleteCommandHandler : IRequestHandler<ProductPermissionGroupBatchDeleteCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public ProductPermissionGroupBatchDeleteCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IIdentityService identityService)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(request.Ids)) return Unit.Value;

            var operatorId = identityService.GetUserId();
            var idArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var id in idArr)
            {
                var data = await productPermissionGroupRepository.FindAsync(id);
                await productPermissionGroupRepository.DeleteAsync(data, operatorId);
            }

            return Unit.Value;
        } 
        #endregion
    }
}
