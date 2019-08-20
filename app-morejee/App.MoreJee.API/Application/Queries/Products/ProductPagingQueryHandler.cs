using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Consts;
using App.Basic.Export;
using App.MoreJee.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using App.MoreJee.Infrastructure.Specifications.ProductSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductPagingQueryHandler : IRequestHandler<ProductPagingQuery, PagingQueryResult<ProductPagingQueryDTO>>
    {
        private readonly IProductRepository productRepository;
        private readonly ICategoryRepository categoryRepository;
        private readonly IIdentityService identityService;
        private readonly AccountService accountService;
        private readonly IClientAssetPermissionControlService clientAssetPermissionControlService;

        #region ctor
        public ProductPagingQueryHandler(IProductRepository productRepository, ICategoryRepository categoryRepository, IIdentityService identityService, AccountService accountService, IClientAssetPermissionControlService clientAssetPermissionControlService)
        {
            this.productRepository = productRepository;
            this.categoryRepository = categoryRepository;
            this.identityService = identityService;
            this.accountService = accountService;
            this.clientAssetPermissionControlService = clientAssetPermissionControlService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<ProductPagingQueryDTO>> Handle(ProductPagingQuery request, CancellationToken cancellationToken)
        {
            var result = new PagingQueryResult<ProductPagingQueryDTO>();
            request.CheckPagingParam();

            if (!request.UnClassified)
                request.CategoryId = await categoryRepository.GetAllSubCategoryIds(request.CategoryId);

            var currentOrganId = identityService.GetOrganizationId();
            var clientOrganId = await clientAssetPermissionControlService.ClientAssetOrganIdRedirection();

            var pointKey = await accountService.GetAccessPoint();
            var showPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PriceRetrieve);
            var showPartnerPrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PartnerPriceRetrieve);
            var showPurchasePrice = pointKey.Keys.Any(k => k == AccessPointInnerPointKeyConst.PurchasePriceRetrieve);

            var specification = new ProductPagingSpecification(currentOrganId, request.Page, request.PageSize, request.Search, request.OrderBy, request.Desc, request.CategoryId, request.UnClassified);
            if (currentOrganId != clientOrganId)
                specification.SignPermissionPaging();

            var datas = await productRepository.Paging(specification).ToListAsync();
            var dtos = datas.Select(x => ProductPagingQueryDTO.From(x)).ToList();
            for (var idx = dtos.Count - 1; idx >= 0; idx--)
            {
                var it = dtos[idx];
                it.CategoryName = await categoryRepository.GetCategoryName(it.CategoryId);
                if (!showPrice)
                    it.HidePrice();

                if (!showPartnerPrice)
                    it.HidePartnerPrice();

                if (!showPurchasePrice)
                    it.HidePurchasePrice();
            }
            result.Total = await productRepository.Get(specification).CountAsync();
            result.Data = dtos;
            return result;
        }
        #endregion
    }
}
