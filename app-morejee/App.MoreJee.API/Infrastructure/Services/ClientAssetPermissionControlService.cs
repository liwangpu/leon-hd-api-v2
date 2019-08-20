using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Consts;
using App.Basic.Export;
using System.Threading.Tasks;

namespace App.MoreJee.API.Infrastructure.Services
{
    public interface IClientAssetPermissionControlService
    {
        Task<string> ClientAssetOrganIdRedirection();
        Task<bool> CanEditClientAsset();
    }

    public class ClientAssetPermissionControlService : IClientAssetPermissionControlService
    {
        private readonly IIdentityService identityService;
        private readonly OrganizationService organizationService;
        private readonly AcessPointKeyService acessPointKeyService;

        public ClientAssetPermissionControlService(IIdentityService identityService, OrganizationService organizationService, AcessPointKeyService acessPointKeyService)
        {
            this.identityService = identityService;
            this.organizationService = organizationService;
            this.acessPointKeyService = acessPointKeyService;
        }

        /// <summary>
        /// 客户端资源Map,StaticMesh等等,在系统中以组织Id记录归属
        /// 用户如果需要访问客户端资源,需要判断当前组织类型
        /// 如果是品牌商组织Id为自己,如果是代理商和供应商,组织Id为父组织Id
        /// </summary>
        /// <returns></returns>
        public async Task<string> ClientAssetOrganIdRedirection()
        {
            return await organizationService.ClientAssetOrganIdRedirection();
        }

        public async Task<bool> CanEditClientAsset()
        {
            return await acessPointKeyService.CheckExistPointKey(identityService.GetUserId(), AccessPointInnerPointKeyConst.ClientAssetManagement);
        }


    }
}
