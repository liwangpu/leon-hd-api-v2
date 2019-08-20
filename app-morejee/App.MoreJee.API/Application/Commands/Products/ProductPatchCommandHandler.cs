using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductPatchCommandHandler : IRequestHandler<ProductPatchCommand>
    {
        private readonly IIdentityService identityService;
        private readonly IProductRepository productRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public ProductPatchCommandHandler(IIdentityService identityService, IProductRepository productRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.identityService = identityService;
            this.productRepository = productRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(ProductPatchCommand request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindAsync(request.Id);
            if (product == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Product", request.Id]);

            mapper.Map(product, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            product.UpdateBasicInfo(request.Name, request.Description, request.Brand, request.Unit, modifier);
            product.UpdateCategory(request.CategoryId);
            await productRepository.UpdateAsync(product);
            return Unit.Value;
        }
        #endregion

    }
}
