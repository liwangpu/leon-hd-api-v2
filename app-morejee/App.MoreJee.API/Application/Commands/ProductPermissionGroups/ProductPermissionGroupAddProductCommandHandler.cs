using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupAddProductCommandHandler : IRequestHandler<ProductPermissionGroupAddProductCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        #region ctor
        public ProductPermissionGroupAddProductCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupAddProductCommand request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.ProductPermissionGroupId);
            await productPermissionGroupRepository.LoadOwnOrganItemsAsync(data);

            var organIdArr = request.ProductIds.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var organId in organIdArr)
                data.AddOwnProduct(organId);

            await productPermissionGroupRepository.UpdateAsync(data);
            return Unit.Value;
        } 
        #endregion
    }
}
