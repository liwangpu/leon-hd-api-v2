using App.MoreJee.Export.Models;
using Flurl;
using Flurl.Http;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.MoreJee.Export
{
    public class ProductService : ServiceBase
    {
        #region ctor
        public ProductService(string server, string auth)
        : base(server, auth)
        {

        }
        #endregion

        public async Task<ProductBriefIdentityQueryDTO> GetBriefById(string id)
        {
            var api = $"{Server}/MoreJee/Products/{id}/Brief";
            return await api.AllowHttpStatus().GetJsonAsync<ProductBriefIdentityQueryDTO>();
        }

        public async Task<List<ProductBriefIdentityQueryDTO>> GetBriefByIds(string ids)
        {
            if (string.IsNullOrWhiteSpace(ids)) return new List<ProductBriefIdentityQueryDTO>();

            var api = $"{Server}/MoreJee/Products/Brief";
            return await api.SetQueryParam("ids", ids).AllowHttpStatus().GetJsonAsync<List<ProductBriefIdentityQueryDTO>>();
        }

        public async Task<List<ProductBriefIdentityQueryDTO>> GetBriefByIds(IEnumerable<string> ids)
        {
            if (ids == null || ids.Count() == 0) return new List<ProductBriefIdentityQueryDTO>();

            var idStr = string.Join(",", ids);
            return await GetBriefByIds(idStr);
        }
    }
}
