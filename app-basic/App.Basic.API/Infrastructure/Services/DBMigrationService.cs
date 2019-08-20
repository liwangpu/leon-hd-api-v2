using App.Base.Domain.Consts;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Infrastructure;
using App.Basic.Infrastructure.Specifications.AccessPointSpecifications;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Infrastructure.Services
{
    public class DBMigrationService : IHostedService
    {
        private readonly BasicAppContext context;
        private readonly IOrganizationRepository organizationRepository;
        private readonly IAccessPointRepository accessPointRepository;
        private readonly AppConfig appConfig;
        public DBMigrationService(BasicAppContext context, IOptions<AppConfig> options, IOrganizationRepository organizationRepository, IAccessPointRepository accessPointRepository)
        {
            this.context = context;
            this.organizationRepository = organizationRepository;
            this.accessPointRepository = accessPointRepository;
            appConfig = options.Value;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {

            //#if !DEBUG
            context.Database.Migrate();

            #region 创建默认组织和用户
            {
                var softWareOrgan = await organizationRepository.FindAsync(DomainEntityDefaultIdConst.SoftwareProviderOrganizationId);

                //await organizationRepository.Entry(softWareOrgan).Collection(x => x.OwnAccounts).LoadAsync();

                if (softWareOrgan == null)
                {
                    var softwareProviderOrgan = new Organization(OrganizationType.ServiceProvider, appConfig.SoftwareProviderSettings.Name, "默认组织", appConfig.SoftwareProviderSettings.Mail, appConfig.SoftwareProviderSettings.Phone, DomainEntityDefaultIdConst.SoftwareProviderAdminId);
                    softwareProviderOrgan.CustomizeId(DomainEntityDefaultIdConst.SoftwareProviderOrganizationId);
                    await organizationRepository.AddAsync(softwareProviderOrgan);
                }
            }

            #endregion

            #region 创建默认的权限点
            var hasProductManagement = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.ProductBasicInfoManagement)).AnyAsync();
            if (!hasProductManagement)
            {
                var accPoint = new AccessPoint("AccessPoint.ProductBasicInfoManagement", AccessPointInnerPointKeyConst.ProductBasicInfoManagement, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasRetrievePrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PriceRetrieve)).AnyAsync();
            if (!hasRetrievePrice)
            {
                var accPoint = new AccessPoint("AccessPoint.RetrievePrice", AccessPointInnerPointKeyConst.PriceRetrieve, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasEditPrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PriceEdit)).AnyAsync();
            if (!hasEditPrice)
            {
                var accPoint = new AccessPoint("AccessPoint.PriceEdit", AccessPointInnerPointKeyConst.PriceEdit, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasRetrievePartnerPrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PartnerPriceRetrieve)).AnyAsync();
            if (!hasRetrievePartnerPrice)
            {
                var accPoint = new AccessPoint("AccessPoint.RetrievePartnerPrice", AccessPointInnerPointKeyConst.PartnerPriceRetrieve, string.Empty, new List<int> { OrganizationType.Brand.Id, OrganizationType.Partner.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasEditPartnerPrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PartnerPriceEdit)).AnyAsync();
            if (!hasEditPartnerPrice)
            {
                var accPoint = new AccessPoint("AccessPoint.PartnerPriceEdit", AccessPointInnerPointKeyConst.PartnerPriceEdit, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasRetrievePurchasePrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PurchasePriceRetrieve)).AnyAsync();
            if (!hasRetrievePurchasePrice)
            {
                var accPoint = new AccessPoint("AccessPoint.RetrievePurchasePrice", AccessPointInnerPointKeyConst.PurchasePriceRetrieve, string.Empty, new List<int> { OrganizationType.Brand.Id, OrganizationType.Supplier.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasEditPurchasePrice = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.PurchasePriceEdit)).AnyAsync();
            if (!hasEditPurchasePrice)
            {
                var accPoint = new AccessPoint("AccessPoint.PurchasePriceEdit", AccessPointInnerPointKeyConst.PurchasePriceEdit, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }

            var hasClientAssetManagement = await accessPointRepository.Get(new PointKeyUniqueCheckSpecification(AccessPointInnerPointKeyConst.ClientAssetManagement)).AnyAsync();
            if (!hasClientAssetManagement)
            {
                var accPoint = new AccessPoint("AccessPoint.ClientAssetManagement", AccessPointInnerPointKeyConst.ClientAssetManagement, string.Empty, new List<int> { OrganizationType.Brand.Id });
                accPoint.SignInner();
                await accessPointRepository.AddAsync(accPoint);
            }




            #endregion
            //#endif
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }
}
