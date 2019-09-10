using App.Base.API.Application.Queries;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<AccountPagingQueryDTO>>
    {
        public string Mail { get; set; }
        public string Phone { get; set; }
    }

    public class AccountPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }

        public long ModifiedTime { get; set; }

        public static AccountPagingQueryDTO From(Account acc)
        {
            return new AccountPagingQueryDTO
            {
                Id = acc.Id,
                Name = acc.Name,
                Description = acc.Description,
                Mail = acc.Mail,
                Phone = acc.Phone,
                ModifiedTime = acc.ModifiedTime
            };
        }
    }
}
