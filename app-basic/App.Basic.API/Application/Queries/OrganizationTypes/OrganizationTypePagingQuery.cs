using App.Base.API.Application.Queries;
using MediatR;

namespace App.Basic.API.Application.Queries.OrganizationTypes
{
    public class OrganizationTypePagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<OrganizationTypePagingQueryDTO>>
    { }

    public class OrganizationTypePagingQueryDTO
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
    }
}
