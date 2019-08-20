using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.ProductSpecs
{
    public class ProductSpecBriefIdentityQueryHandler : IRequestHandler<ProductSpecBriefIdentityQuery, ProductSpecBriefIdentityQueryDTO>
    {
        private readonly IProductSpecRepository productSpecRepository;
        private readonly IMapper mapper;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public ProductSpecBriefIdentityQueryHandler(IProductSpecRepository productSpecRepository, IMapper mapper, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.productSpecRepository = productSpecRepository;
            this.mapper = mapper;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<ProductSpecBriefIdentityQueryDTO> Handle(ProductSpecBriefIdentityQuery request, CancellationToken cancellationToken)
        {
            var spec = await productSpecRepository.FindAsync(request.Id);
            if (spec == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "ProductSpec", request.Id]);

            return mapper.Map<ProductSpecBriefIdentityQueryDTO>(spec);
        } 
        #endregion
    }
}
