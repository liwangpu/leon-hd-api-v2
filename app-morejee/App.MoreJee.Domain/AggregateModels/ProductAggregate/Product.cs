using App.MoreJee.Domain.Events.ProductEvents;
using App.MoreJee.Domain.SeedWork;
using System;
using System.Collections.Generic;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public class Product : Entity, IAggregateRoot
    {
        private readonly List<ProductSpec> _ownProductSpecs;
        public IReadOnlyCollection<ProductSpec> OwnProductSpecs => _ownProductSpecs;
        public string Name { get; protected set; }
        public string Description { get; protected set; }
        public string Creator { get; protected set; }
        public string Modifier { get; protected set; }
        public long CreatedTime { get; protected set; }
        public long ModifiedTime { get; protected set; }
        public string OrganizationId { get; protected set; }
        public string DefaultProductSpecId { get; protected set; }
        public string CategoryId { get; protected set; }
        public string Icon { get; protected set; }
        /// <summary>
        /// 冗余字段,最高零售价,是所有产品规格里面最高的零售价
        /// </summary>
        public decimal MaxPrice { get; protected set; }
        /// <summary>
        /// 冗余字段,最低零售价
        /// </summary>
        public decimal MinPrice { get; protected set; }
        public decimal MaxPartnerPrice { get; protected set; }
        public decimal MinPartnerPrice { get; protected set; }
        public decimal MaxPurchasePrice { get; protected set; }
        public decimal MinPurchasePrice { get; protected set; }

        public string Brand { get; protected set; }
        public string Unit { get; protected set; }

        #region ctor
        protected Product()
        {

            _ownProductSpecs = new List<ProductSpec>();
        }

        public Product(string name, string description, string brand, string unit, string organizationId, string creator, string sourcedStaticMeshId = null)
               : this()
        {
            Name = name;
            Description = description;
            Brand = brand;
            Unit = unit;
            Creator = creator;
            Modifier = Creator;
            CreatedTime = DateTime.UtcNow.ToUnixTimeSeconds();
            ModifiedTime = CreatedTime;
            OrganizationId = organizationId;
            AddDomainEvent(new ProductCreatedEvent(Id, Name, sourcedStaticMeshId, OrganizationId, Creator));
        }
        #endregion

        public void UpdateBasicInfo(string name, string description, string brand, string unit, string modifier)
        {
            Name = name;
            Description = description;
            Brand = brand;
            Unit = unit;
            Modifier = modifier;
            ModifiedTime = DateTime.UtcNow.ToUnixTimeSeconds();
        }

        public void SetDefaultProductSpec(ProductSpec spec)
        {
            DefaultProductSpecId = spec.Id;
            Icon = spec.Icon;
        }

        public void UpdatePriceRate(decimal minPrice, decimal maxPrice)
        {
            MinPrice = minPrice;
            MaxPrice = maxPrice;
        }

        public void UpdatePartnerPriceRate(decimal minPartnerPrice, decimal maxPartnerPrice)
        {
            MinPartnerPrice = minPartnerPrice;
            MaxPartnerPrice = maxPartnerPrice;
        }

        public void UpdatePurchasePriceRate(decimal minPurchasePrice, decimal maxPurchasePrice)
        {
            MinPurchasePrice = minPurchasePrice;
            MaxPurchasePrice = maxPurchasePrice;
        }


        public void UpdateIcon(string icon)
        {
            Icon = icon;
        }

        public void UpdateCategory(string categoryId)
        {
            CategoryId = categoryId;
        }

    }
}
