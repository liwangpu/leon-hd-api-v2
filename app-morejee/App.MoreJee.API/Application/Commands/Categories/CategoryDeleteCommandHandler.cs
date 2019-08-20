using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryDeleteCommandHandler : IRequestHandler<CategoryDeleteCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IStringLocalizer<MoreJeeAppTranslation> appLocalizer;

        #region ctor
        public CategoryDeleteCommandHandler(ICategoryRepository categoryRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<MoreJeeAppTranslation> appLocalizer)
        {
            this.categoryRepository = categoryRepository;
            this.commonLocalizer = commonLocalizer;
            this.appLocalizer = appLocalizer;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(CategoryDeleteCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.Id);
            if (category == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.Id]);

            //如果有子节点,禁止删除
            if (category.RValue - category.LValue > 1)
                throw new HttpBadRequestException(appLocalizer["Category.CannotDeleteWithChildCategory"]);

            //其他逻辑
            await categoryRepository.DeleteAsync(request.Id, string.Empty);
            return Unit.Value;
        } 
        #endregion
    }
}
