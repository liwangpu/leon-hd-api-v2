using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryIdentityQueryHandler : IRequestHandler<CategoryIdentityQuery, CategoryIdentityQueryDTO>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;

        #region ctor
        public CategoryIdentityQueryHandler(ICategoryRepository categoryRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
        }
        #endregion

        #region Handler
        public async Task<CategoryIdentityQueryDTO> Handle(CategoryIdentityQuery request, CancellationToken cancellationToken)
        {
            var category = await categoryRepository.FindAsync(request.Id);
            if (category == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Category", request.Id]);

            return mapper.Map<CategoryIdentityQueryDTO>(category);
        }
        #endregion
    }
}
