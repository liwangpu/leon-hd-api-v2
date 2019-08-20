using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryHierarchyChangeCommandHandler : IRequestHandler<CategoryHierarchyChangeCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public CategoryHierarchyChangeCommandHandler(ICategoryRepository categoryRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.categoryRepository = categoryRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(CategoryHierarchyChangeCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.CategoryId);
            if (category == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.CategoryId]);
            var parentCategory = await categoryRepository.FindAsync(request.ParentId);
            if (parentCategory == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.ParentId]);

            await categoryRepository.ChangeHierarchyAsync(request.CategoryId, request.ParentId);

            return Unit.Value;
        } 
        #endregion
    }
}
