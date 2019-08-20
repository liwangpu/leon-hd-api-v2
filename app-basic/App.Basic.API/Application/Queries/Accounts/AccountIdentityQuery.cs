using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountIdentityQuery : IRequest<AccountIdentityQueryDTO>
    {
        public string Id { get; set; }
    }

    public class AccountIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public string OrganizationId { get; set; }

        public static AccountIdentityQueryDTO From(Account account)
        {
            return new AccountIdentityQueryDTO
            {
                Id = account.Id,
                Name = account.Name,
                FirstName = account.FirstName,
                LastName = account.LastName,
                Mail = account.Mail,
                Phone = account.Phone,
                Creator = account.Creator,
                Modifier = account.Modifier,
                CreatedTime = account.CreatedTime,
                ModifiedTime = account.ModifiedTime,
                OrganizationId = account.OrganizationId
            };
        }
    }

}
