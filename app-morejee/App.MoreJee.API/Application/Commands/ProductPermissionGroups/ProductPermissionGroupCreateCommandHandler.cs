using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupCreateCommandHandler : IRequestHandler<ProductPermissionGroupCreateCommand, string>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public ProductPermissionGroupCreateCommandHandler(IProductPermissionGroupRepository productPermissionGroupRepository, IIdentityService identityService)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(ProductPermissionGroupCreateCommand request, CancellationToken cancellationToken)
        {
            var group = new ProductPermissionGroup(request.Name, request.Description, identityService.GetOrganizationId());
            await productPermissionGroupRepository.AddAsync(group);

            return group.Id;
        }
        #endregion
    }
}
