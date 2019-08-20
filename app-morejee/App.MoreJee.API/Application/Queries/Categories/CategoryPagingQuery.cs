using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<CategoryPagingQueryDTO>>
    {

    }

    public class CategoryPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string Resource { get; set; }
    }
}
