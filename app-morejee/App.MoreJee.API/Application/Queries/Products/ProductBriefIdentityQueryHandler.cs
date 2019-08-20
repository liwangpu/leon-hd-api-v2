using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductBriefIdentityQueryHandler : IRequestHandler<ProductBriefIdentityQuery, ProductBriefIdentityQueryDTO>
    {
        private readonly IProductRepository productRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly ICategoryRepository categoryRepository;

        #region ctor
        public ProductBriefIdentityQueryHandler(IProductRepository productRepository, IStringLocalizer<CommonTranslation> commonLocalizer, ICategoryRepository categoryRepository)
        {
            this.productRepository = productRepository;
            this.commonLocalizer = commonLocalizer;
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region Handle
        public async Task<ProductBriefIdentityQueryDTO> Handle(ProductBriefIdentityQuery request, CancellationToken cancellationToken)
        {
            var product = await productRepository.FindAsync(request.Id);
            if (product == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Product", request.Id]);

            var dto = ProductBriefIdentityQueryDTO.From(product);
            dto.CategoryName = await categoryRepository.GetCategoryName(product.CategoryId);
            return dto;
        } 
        #endregion
    }
}
