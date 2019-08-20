using App.Basic.Export.Models;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Basic.Export
{
    public class OrganizationService : ServiceBase
    {
        #region ctor
        public OrganizationService(string server, string auth)
         : base(server, auth)
        {

        }
        #endregion

        public async Task<List<OrganizationBriefIdentitiesQueryDTO>> GetBriefByIds(string ids)
        {
            var api = $"{Server}/Basic/Organizations/Brief";
            return await api.WithOAuthBearerToken(Token).SetQueryParam("ids", ids).AllowHttpStatus().GetJsonAsync<List<OrganizationBriefIdentitiesQueryDTO>>();
        }

        public async Task<string> ClientAssetOrganIdRedirection()
        {
            var api = $"{Server}/Basic/Organizations/ClientAssetOrganIdRedirection";
            return await api.WithOAuthBearerToken(Token).AllowHttpStatus().GetJsonAsync<string>();
        }
    }
}
