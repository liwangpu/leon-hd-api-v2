using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupDeleteOrganCommandHandler : IRequestHandler<ProductPermissionGroupDeleteOrganCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;

        #region ctor
        public ProductPermissionGroupDeleteOrganCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupDeleteOrganCommand request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.ProductPermissionGroupId);
            await productPermissionGroupRepository.LoadOwnOrganItemsAsync(data);

            var itIdArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var itId in itIdArr)
                data.DeleteOwnOrganization(itId);
            await productPermissionGroupRepository.UpdateAsync(data);
            return Unit.Value;
        }
        #endregion
    }
}
