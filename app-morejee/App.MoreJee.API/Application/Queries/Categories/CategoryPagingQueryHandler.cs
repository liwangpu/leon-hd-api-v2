using App.Base.API.Application.Queries;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Infrastructure.Specifications.CategorySpecifications;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryPagingQueryHandler : IRequestHandler<CategoryPagingQuery, PagingQueryResult<CategoryPagingQueryDTO>>
    {
        private readonly ICategoryRepository categoryRepository;
        private readonly IMapper mapper;

        #region ctor
        public CategoryPagingQueryHandler(ICategoryRepository categoryRepository, IMapper mapper)
        {
            this.categoryRepository = categoryRepository;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<CategoryPagingQueryDTO>> Handle(CategoryPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<CategoryPagingQueryDTO>();
            request.CheckPagingParam();

            var specification = new CategoryPagingSpecification(request.Page, request.PageSize, request.Search);
            var data = await categoryRepository.Paging(specification).ToListAsync();
            result.Total = await categoryRepository.Get(specification).CountAsync();
            result.Data = data.Select(x => mapper.Map<CategoryPagingQueryDTO>(x)).ToList();

            return result;
        } 
        #endregion
    }
}
