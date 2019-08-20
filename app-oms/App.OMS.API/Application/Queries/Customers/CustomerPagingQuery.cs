using App.Base.API.Application.Queries;
using MediatR;

namespace App.OMS.API.Application.Queries.Customers
{
    public class CustomerPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<CustomerPagingQueryDTO>>
    {
    }

    public class CustomerPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Company { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
    }
}
