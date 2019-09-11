using App.MoreJee.Domain.Events.ProductEvents;
using App.MoreJee.Domain.SeedWork;
using System;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class ProductSpec : Entity
    {
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string ProductId { get; protected set; }
        public Product Product { get; protected set; }
        /// <summary>
        /// 零售价，单位为元
        /// </summary>
        public decimal Price { get; protected set; }
        /// <summary>
        /// 合伙人价格|渠道价，单位为元
        /// </summary>
        public decimal PartnerPrice { get; protected set; }
        /// <summary>
        /// 进货价，单位为元
        /// </summary>
        public decimal PurchasePrice { get; protected set; }
        /// <summary>
        /// 冗余字段,IconAssetId对应的url
        /// </summary>
        public string Icon { get; protected set; }
        /// <summary>
        /// 逗号分隔的Id,记录着这个规格所引用的StaticMesh Ids信息
        /// </summary>
        public string RelatedStaticMeshIds { get; protected set; }

        #region ctor
        protected ProductSpec()
        {

        }

        public ProductSpec(string name, string description, string productId, string organizationId, string creator, string staticMeshId = null)
            : this()
        {
            Name = name;
            Description = description;
            ProductId = productId;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            OrganizationId = organizationId;
            RelatedStaticMeshIds = staticMeshId;
            AddDomainEvent(new ProductSpecCreatedEvent(Id, Name, staticMeshId, OrganizationId, Creator));
        }
        #endregion

        public void UpdateBasicInfo(string name, string description, string modifier)
        {
            Name = name;
            Description = description;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }

        public void UpdateIcon(string icon)
        {
            var bIconChange = Icon != icon;
            Icon = icon;
            if (bIconChange)
                AddDomainEvent(new ProductSpecIconUpdatedEvent(ProductId, Id, Icon));
        }

        public void UpdatePriceInfo(decimal price, decimal partnerPrice, decimal purchasePrice)
        {
            var bPriceChange = Price != price;
            var bPartnerPriceChange = PartnerPrice != partnerPrice;
            var bPurchasePriceChange = PurchasePrice != purchasePrice;
            Price = price;
            PartnerPrice = partnerPrice;
            PurchasePrice = purchasePrice;

            if (bPriceChange || bPartnerPriceChange || bPurchasePriceChange)
                AddDomainEvent(new ProductSpecPriceUpdatedEvent(ProductId, Id, Price, PartnerPrice, PurchasePrice));
        }

        public void DeleteRelatedStaticMesh()
        {
            if (!string.IsNullOrWhiteSpace(RelatedStaticMeshIds))
                AddDomainEvent(new DeleteProductSpecRelatedStaticMeshEvent(RelatedStaticMeshIds));
        }

        //public void SignRelateddStaticMesh(string staticMeshId)
        //{
        //    if (string.IsNullOrWhiteSpace(staticMeshId))
        //        return;


        //    var list = string.IsNullOrWhiteSpace(RelatedStaticMeshIds) ? new List<string>() : RelatedStaticMeshIds.Split(",", StringSplitOptions.RemoveEmptyEntries).ToList();

        //    if (!list.Any(x => x == staticMeshId))
        //        list.Add(staticMeshId);

        //    RelatedStaticMeshIds = string.Join(",", list);
        //}
    }
}
