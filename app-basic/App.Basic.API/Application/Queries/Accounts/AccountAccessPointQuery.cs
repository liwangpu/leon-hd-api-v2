using MediatR;
using System.Linq;

using System.Collections.Generic;

namespace App.Basic.API.Application.Queries.Accounts
{
    public class AccountAccessPointQuery : IRequest<AccountAccessPointQueryDTO>
    {

    }

    public class AccountAccessPointQueryDTO
    {
        public List<string> Keys { get; set; }

        public AccountAccessPointQueryDTO()
        {
            Keys = new List<string>();
        }

        public void DistinctKey()
        {
            if (Keys.Count == 0) return;

            Keys = Keys.Distinct().ToList();
        }
    }
}
