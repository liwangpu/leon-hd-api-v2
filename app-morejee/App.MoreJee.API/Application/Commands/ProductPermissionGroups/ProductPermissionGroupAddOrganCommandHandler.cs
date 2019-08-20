using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupAddOrganCommandHandler : IRequestHandler<ProductPermissionGroupAddOrganCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;

        #region ctor
        public ProductPermissionGroupAddOrganCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPermissionGroupAddOrganCommand request, CancellationToken cancellationToken)
        {
            var data = await productPermissionGroupRepository.FindAsync(request.ProductPermissionGroupId);
            await productPermissionGroupRepository.LoadOwnOrganItemsAsync(data);

            var organIdArr = request.OrganizationIds.Split(",", StringSplitOptions.RemoveEmptyEntries);
            foreach (var organId in organIdArr)
                data.AddOwnOrganization(organId);

            await productPermissionGroupRepository.UpdateAsync(data);

            return Unit.Value;
        }
        #endregion
    }
}
