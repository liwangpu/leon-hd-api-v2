using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductIdentityQuery : IRequest<ProductIdentityQueryDTO>
    {
        public string Id { get; protected set; }
        public ProductIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class ProductIdentityQueryDTO
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
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPartnerPrice { get; set; }
        public decimal MinPartnerPrice { get; set; }
        public decimal MaxPurchasePrice { get; set; }
        public decimal MinPurchasePrice { get; set; }
        public string Price
        {
            get
            {
                if (MinPrice == MaxPrice && MinPrice == 0)
                    return "0";

                if (MinPrice == MaxPrice)
                    return $"{MinPrice}";

                if (MinPrice != 0 && MaxPrice != 0)
                    return $"{MinPrice}-{MaxPrice}";

                return string.Empty;
            }
        }

        public string PartnerPrice
        {
            get
            {
                if (MinPartnerPrice == MaxPartnerPrice && MinPartnerPrice == 0)
                    return "0";

                if (MinPartnerPrice == MaxPartnerPrice)
                    return $"{MinPartnerPrice}";

                if (MinPartnerPrice != 0 && MaxPartnerPrice != 0)
                    return $"{MinPartnerPrice}-{MaxPartnerPrice}";

                return string.Empty;
            }
        }

        public string PurchasePrice
        {
            get
            {
                if (MinPurchasePrice == MaxPurchasePrice && MinPurchasePrice == 0)
                    return "0";

                if (MinPurchasePrice == MaxPurchasePrice)
                    return $"{MinPurchasePrice}";

                if (MinPurchasePrice != 0 && MaxPurchasePrice != 0)
                    return $"{MinPurchasePrice }-{MaxPurchasePrice }";

                return string.Empty;
            }
        }

        public void HidePrice()
        {
            MinPrice = 0;
            MaxPrice = 0;
            if (Specifications != null && Specifications.Count > 0)
            {
                for (int idx = Specifications.Count - 1; idx >= 0; idx--)
                {
                    var it = Specifications[idx];
                    it.Price = 0;
                }
            }
        }

        public void HidePartnerPrice()
        {
            MinPartnerPrice = 0;
            MaxPartnerPrice = 0;
            if (Specifications != null && Specifications.Count > 0)
            {
                for (int idx = Specifications.Count - 1; idx >= 0; idx--)
                {
                    var it = Specifications[idx];
                    it.PartnerPrice = 0;
                }
            }
        }

        public void HidePurchasePrice()
        {
            MinPurchasePrice = 0;
            MaxPurchasePrice = 0;
            if (Specifications != null && Specifications.Count > 0)
            {
                for (int idx = Specifications.Count - 1; idx >= 0; idx--)
                {
                    var it = Specifications[idx];
                    it.PurchasePrice = 0;
                }
            }
        }

        public List<ProductSpecificationDTO> Specifications = new List<ProductSpecificationDTO>();

        public static ProductIdentityQueryDTO From(Product product)
        {
            var specs = product.OwnProductSpecs.Select(x => ProductSpecificationDTO.From(x)).ToList();
            return new ProductIdentityQueryDTO
            {
                Id = product.Id,
                Name = product.Name,
                Icon = product.Icon,
                CategoryId = product.CategoryId,
                Description = product.Description,
                Creator = product.Creator,
                Modifier = product.Modifier,
                CreatedTime = product.CreatedTime,
                ModifiedTime = product.ModifiedTime,
                OrganizationId = product.OrganizationId,
                Brand=product.Brand,
                Unit = product.Unit,
                Specifications = specs,
                MinPrice = product.MinPrice,
                MaxPrice = product.MaxPrice,
                MinPartnerPrice = product.MinPartnerPrice,
                MaxPartnerPrice = product.MaxPartnerPrice,
                MinPurchasePrice = product.MinPurchasePrice,
                MaxPurchasePrice = product.MaxPurchasePrice
            };
        }

        public class ProductSpecificationDTO
        {
            /// <summary>
            /// Id
            /// </summary>
            public string Id { get; set; }
            /// <summary>
            /// 规格名称
            /// </summary>
            public string Name { get; set; }
            /// <summary>
            /// 图标
            /// </summary>
            public string Icon { get; set; }
            /// <summary>
            /// 描述
            /// </summary>
            public string Description { get; set; }
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

            public static ProductSpecificationDTO From(ProductSpec spec)
            {
                return new ProductSpecificationDTO
                {
                    Id = spec.Id,
                    Name = spec.Name,
                    Icon = spec.Icon,
                    Description = spec.Description,
                    Price = spec.Price,
                    PartnerPrice = spec.PartnerPrice,
                    PurchasePrice = spec.PurchasePrice
                };
            }
        }
    }


}
