using App.Basic.Export.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace App.Basic.Export
{
    public class AcessPointKeyService : ServiceBase
    {
        #region ctor
        public AcessPointKeyService(string server, string auth)
         : base(server, auth)
        {
        }
        #endregion

        public async Task<bool> CheckExistPointKey(string userId, string pointKey)
        {
            var api = $"{Server}/Basic/AccessPoints/Check";
            return await api.WithOAuthBearerToken(Token).SetQueryParams(new { UserId = userId, PointKey = pointKey }).AllowHttpStatus().GetJsonAsync<bool>();
        }

    }
}
