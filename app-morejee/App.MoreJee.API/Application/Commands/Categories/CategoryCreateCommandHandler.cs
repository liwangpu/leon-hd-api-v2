using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryCreateCommandHandler : IRequestHandler<CategoryCreateCommand, string>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public CategoryCreateCommandHandler(ICategoryRepository categoryRepository, IIdentityService identityService)
        {
            this.categoryRepository = categoryRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(CategoryCreateCommand request, CancellationToken cancellationToken)
        {
            request.CheckResource();

            var category = new Category(request.Name, request.Description, request.NodeType, request.Resource, identityService.GetOrganizationId(), identityService.GetUserId(), request.ParentId, request.Icon);
            await categoryRepository.AddAsync(category);
            return category.Id;
        } 
        #endregion
    }
}
