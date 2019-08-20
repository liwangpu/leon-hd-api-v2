using App.Base.API.Application.Queries;
using MediatR;
using System;

namespace App.MoreJee.API.Application.Queries.Solutions
{
    public class SolutionPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<SolutionPagingQueryDTO>>
    {
    }

    public class SolutionPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
    }
}
