using App.Base.API;
using App.Base.API.Infrastructure.ActionResults;
using App.Base.API.Infrastructure.Extensions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductBatchDeleteCommandHandler : IRequestHandler<ProductBatchDeleteCommand, ObjectResult>
    {
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IUriService uriService;
        private readonly IIdentityService identityService;
        private readonly IProductRepository productRepository;

        #region ctor
        public ProductBatchDeleteCommandHandler(IStringLocalizer<CommonTranslation> localizer, IUriService uriService, IIdentityService identityService, IProductRepository productRepository)
        {
            this.localizer = localizer;
            this.uriService = uriService;
            this.identityService = identityService;
            this.productRepository = productRepository;
        }
        #endregion

        #region Handle
        public async Task<ObjectResult> Handle(ProductBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var result = new MultiStatusObjectResult();
            var operatorId = identityService.GetUserId();
            var resourcePartUri = uriService.GetUriWithoutQuery().URIUpperLevel();
            var idArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, len = idArr.Count(); i < len; i++)
            {
                var id = idArr[i];
                var uri = $"{resourcePartUri}/{id}";

                var data = await productRepository.FindAsync(id);
                if (data == null)
                {
                    result.AddResult(uri, 404, "");
                    continue;
                }



                await productRepository.DeleteAsync(data, operatorId);
                result.AddResult(uri, 200, "");
            }

            return result.Transfer();
        } 
        #endregion
    }
}
