using App.Base.API;
using App.MoreJee.API.Application.Commands.ProductPermissionGroups;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;


namespace App.MoreJee.API.Application.Validations.ProdutPermissionGroups
{
    public class ProductPermissionGroupAddProductValidator : AbstractValidator<ProductPermissionGroupAddProductCommand>
    {
        private readonly IProductPermissionGroupRepository productPermissionGroupRepository;

        public ProductPermissionGroupAddProductValidator(IStringLocalizer<CommonTranslation> localizer, IProductPermissionGroupRepository productPermissionGroupRepository)
        {
            this.productPermissionGroupRepository = productPermissionGroupRepository;
            RuleFor(cmd => cmd.ProductPermissionGroupId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "ProductPermissionGroupId"]);
            RuleFor(cmd => cmd.ProductIds).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "ProductIds"]);

            RuleFor(x => x.ProductPermissionGroupId).MustAsync(async (id, token) => await ExistGroup(id)).WithMessage(x => localizer["HttpRespond.NotFound", "ProductPermissionGroup", x.ProductPermissionGroupId]);
        }

        public async Task<bool> ExistGroup(string id)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(id)) return true;

            var data = await productPermissionGroupRepository.FindAsync(id);
            return data != null;
        }
    }
}
