using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Infrastructure.Specifications.CategorySpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryChangeDisplayCommandHandler : IRequestHandler<CategoryChangeDisplayCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public CategoryChangeDisplayCommandHandler(ICategoryRepository categoryRepository, IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.categoryRepository = categoryRepository;
            this.identityService = identityService;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(CategoryChangeDisplayCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.Id);
            if (category == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.Id]);

            if (string.IsNullOrWhiteSpace(category.ParentId))
                return Unit.Value;

            var parentCategory = await categoryRepository.FindAsync(category.ParentId);

            var childrenCategories = await categoryRepository.Get(new GetCategoryChildrenSpecification(parentCategory.Id)).ToListAsync();

            if (childrenCategories.Count <= 1)
                return Unit.Value;

            if (request.MoveUp)
            {
                //上移
                var aboveNode = childrenCategories.FirstOrDefault(x => x.DisplayIndex == category.DisplayIndex - 1);
                if (aboveNode != null)
                {
                    aboveNode.SetDisplayIndex(aboveNode.DisplayIndex + 1);
                    category.SetDisplayIndex(category.DisplayIndex - 1);
                    await categoryRepository.UpdateAsync(aboveNode);
                    await categoryRepository.UpdateAsync(category);
                }
            }
            else
            {
                //下移
                var underNode = childrenCategories.FirstOrDefault(x => x.DisplayIndex == category.DisplayIndex + 1);
                if (underNode != null)
                {
                    underNode.SetDisplayIndex(underNode.DisplayIndex - 1);
                    category.SetDisplayIndex(category.DisplayIndex + 1);
                    await categoryRepository.UpdateAsync(underNode);
                    await categoryRepository.UpdateAsync(category);
                }

            }

            return Unit.Value;
        } 
        #endregion
    }
}
