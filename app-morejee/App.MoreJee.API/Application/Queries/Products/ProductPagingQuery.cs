using App.Base.API.Application.Queries;
using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using MediatR;
using System;

namespace App.MoreJee.API.Application.Queries.Products
{
    public class ProductPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<ProductPagingQueryDTO>>
    {
        public string CategoryId { get; set; }
        public bool UnClassified { get; set; }
    }

    public class ProductPagingQueryDTO
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
        public string Price
        {
            get
            {
                if (MinPrice == MaxPrice && MinPrice == 0)
                    return string.Empty;

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
                    return string.Empty;

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
                    return string.Empty;

                if (MinPurchasePrice == MaxPurchasePrice)
                    return $"{MinPurchasePrice}";

                if (MinPurchasePrice != 0 && MaxPurchasePrice != 0)
                    return $"{MinPurchasePrice }-{MaxPurchasePrice }";

                return string.Empty;
            }
        }

        public decimal MaxPrice { get; set; }
        public decimal MinPrice { get; set; }
        public decimal MaxPartnerPrice { get; set; }
        public decimal MinPartnerPrice { get; set; }
        public decimal MaxPurchasePrice { get; set; }
        public decimal MinPurchasePrice { get; set; }

        public void HidePrice()
        {
            MinPrice = 0;
            MaxPrice = 0;
        }

        public void HidePartnerPrice()
        {
            MinPartnerPrice = 0;
            MaxPartnerPrice = 0;
        }

        public void HidePurchasePrice()
        {
            MinPurchasePrice = 0;
            MaxPurchasePrice = 0;
        }

        public static ProductPagingQueryDTO From(Product product)
        {
            return new ProductPagingQueryDTO
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
                Brand = product.Brand,
                Unit = product.Unit,
                MinPrice = product.MinPrice,
                MaxPrice = product.MaxPrice,
                MinPartnerPrice = product.MinPartnerPrice,
                MaxPartnerPrice = product.MaxPartnerPrice,
                MinPurchasePrice = product.MinPurchasePrice,
                MaxPurchasePrice = product.MaxPurchasePrice
            };
        }
    }
}
