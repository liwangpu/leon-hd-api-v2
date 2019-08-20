using App.Base.API;
using App.MoreJee.API.Application.Commands.ProductSpecs;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Validations.Products
{
    public class ProductSpecValidator : AbstractValidator<ProductSpecCreateCommand>
    {
        private readonly IProductRepository productRepository;

        public ProductSpecValidator(IProductRepository productRepository, IStringLocalizer<CommonTranslation> localizer)
        {
            RuleFor(cmd => cmd.ProductId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "ProductId"]);
            this.productRepository = productRepository;
            RuleFor(x => x.ProductId).MustAsync(async (productId, token) => await ExistProduct(productId)).WithMessage(x => localizer["HttpRespond.NotFound", "Product", x.ProductId]);
        }


        public async Task<bool> ExistProduct(string productId)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(productId))
                return true;
            var product = await productRepository.FindAsync(productId);
            return product != null;
        }
    }
}
