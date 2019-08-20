using MediatR;
using System;

namespace App.MoreJee.API.Application.Queries.ProductSpecs
{
    public class ProductSpecIdentityQuery : IRequest<ProductSpecIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public ProductSpecIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class ProductSpecIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public string OrganizationId { get; set; }
        /// <summary>
        /// 零售价
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// 合伙人价格|渠道价
        /// </summary>
        public decimal PartnerPrice { get; set; }
        /// <summary>
        /// 进货价
        /// </summary>
        public decimal PurchasePrice { get; set; }

        public void HidePrice()
        {
            Price = 0;
        }

        public void HidePartnerPrice()
        {
            PartnerPrice = 0;
        }

        public void HidePurchasePrice()
        {
            PurchasePrice = 0;
        }
    }
}
