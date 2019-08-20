using App.Base.API.Application.Queries;
using MediatR;

namespace App.Basic.API.Application.Queries.Roles
{
    public class RolePagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<RolePagingQueryDTO>>
    {
    }

    public class RolePagingQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
        public string AccessPointKeys { get; set; }
    }
}
