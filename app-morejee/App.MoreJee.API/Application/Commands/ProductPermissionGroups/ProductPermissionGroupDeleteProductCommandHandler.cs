using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupDeleteProductCommandHandler : IRequestHandler<ProductPermissionGroupDeleteProductCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;

        #region ctor
        public ProductPermissionGroupDeleteProductCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupDeleteProductCommand request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.ProductPermissionGroupId);
            await productPermissionGroupRepository.LoadOwnProductItemsAsync(data);

            var itIdArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var itId in itIdArr)
                data.DeleteOwnProduct(itId);
            await productPermissionGroupRepository.UpdateAsync(data);
            return Unit.Value;
        }
        #endregion

    }
}
