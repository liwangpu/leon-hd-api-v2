using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Infrastructure.Specifications.CategorySpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryResourceTreeQueryHandler : IRequestHandler<CategoryResourceTreeQuery, string>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public CategoryResourceTreeQueryHandler(ICategoryRepository categoryRepository, IIdentityService identityService)
        {
            this.categoryRepository = categoryRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(CategoryResourceTreeQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.Get(new GetRootCategoryByResourceSpecification(request.Resource)).FirstOrDefaultAsync();

            //根据资源类型查询的话,会默认创建一个资源分类
            if (category == null)
            {
                var cat = new Category(request.Resource, "Automatically created", request.Resource, request.Resource, identityService.GetOrganizationId(), identityService.GetUserId());
                await categoryRepository.AddAsync(cat);
                return cat.Id;
            }

            return category.Id;
        }
        #endregion
    }
}
