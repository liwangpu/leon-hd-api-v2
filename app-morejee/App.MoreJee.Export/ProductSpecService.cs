using App.MoreJee.Export.Models;
using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace App.MoreJee.Export
{
    public class ProductSpecService : ServiceBase
    {
        #region ctor
        public ProductSpecService(string server, string auth)
        : base(server, auth)
        {

        }
        #endregion

        public async Task<ProductSpecBriefIdentityQueryDTO> GetBriefById(string id)
        {
            var api = $"{Server}/MoreJee/ProductSpecs/{id}/Brief";
            return await api.AllowHttpStatus().GetJsonAsync<ProductSpecBriefIdentityQueryDTO>();
        }

        public async Task<List<ProductSpecBriefIdentityQueryDTO>> GetBriefByIds(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids)) return new List<ProductSpecBriefIdentityQueryDTO>();

            var api = $"{Server}/MoreJee/ProductSpecs/Brief";
            return await api.SetQueryParam("ids", ids).AllowHttpStatus().GetJsonAsync<List<ProductSpecBriefIdentityQueryDTO>>();
        }

        public async Task<List<ProductSpecBriefIdentityQueryDTO>> GetBriefByIds(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() == 0) return new List<ProductSpecBriefIdentityQueryDTO>();

            var idStr = string.Join(",", ids);
            return await GetBriefByIds(idStr);
        }
    }
}
