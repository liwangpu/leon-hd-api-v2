using App.Base.API;
using App.MoreJee.API.Application.Commands.Categories;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Validations.Categories
{
    public class CategoryCreateValidator : AbstractValidator<CategoryCreateCommand>
    {
        private readonly ICategoryRepository categoryRepository;

        public CategoryCreateValidator(ICategoryRepository categoryRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.categoryRepository = categoryRepository;
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.NodeType).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "NodeType)"]);
            RuleFor(cmd => cmd.ParentId).MustAsync(async (parentId, token) => await ExistParentCategory(parentId)).WithMessage(cmd => commonLocalizer["HttpRespond.NotFound", "Category", cmd.ParentId]);
        }

        public async Task<bool> ExistParentCategory(string parentId)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(parentId))
                return true;

            var parentCat = await categoryRepository.FindAsync(parentId);
            if (parentCat == null)
                return false;

            return true;
        }
    }
}
