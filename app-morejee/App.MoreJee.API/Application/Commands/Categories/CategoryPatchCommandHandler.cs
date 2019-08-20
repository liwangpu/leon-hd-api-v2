using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryPatchCommandHandler : IRequestHandler<CategoryPatchCommand>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;

        public CategoryPatchCommandHandler(ICategoryRepository categoryRepository, IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.identityService = identityService;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
        }

        public async Task<Unit> Handle(CategoryPatchCommand request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.Id);
            if (category == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.Id]);

            mapper.Map(category, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            category.UpdateBasicInfo(request.Name, request.Description, request.Icon, modifier);
            category.SetResource(request.Resource);
            await categoryRepository.UpdateAsync(category);
            return Unit.Value;
        }
    }
}
