using App.Basic.Export.Models;
using Flurl.Http;
using System.Threading.Tasks;

namespace App.Basic.Export
{
    public class AccountService : ServiceBase
    {
        public AccountService(string server, string auth)
            : base(server, auth)
        {

        }

        public async Task<AccountAccessPointQueryDTO> GetAccessPoint()
        {
            var api = $"{Server}/Basic/Accounts/AccessPointKey";
            return await api.WithOAuthBearerToken(Token).AllowHttpStatus().GetJsonAsync<AccountAccessPointQueryDTO>();
        }
    }
}
