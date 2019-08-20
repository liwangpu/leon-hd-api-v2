using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    /// <summary>
    /// 产品规格的零售价价信息变更事件
    /// </summary>
    public class ProductSpecPriceUpdatedEvent : INotification
    {
        public string ProductSpecId { get; protected set; }
        public string ProductId { get; protected set; }
        public decimal Price { get; protected set; }
        public decimal PartnerPrice { get; protected set; }
        public decimal PurchasePrice { get; protected set; }
        public ProductSpecPriceUpdatedEvent(string productId, string productSpecId, decimal price, decimal partnerPrice, decimal purchasePrice)
        {
            ProductId = productId;
            ProductSpecId = productSpecId;
            Price = price;
            PartnerPrice = partnerPrice;
            PurchasePrice = purchasePrice;
        }
    }
}
