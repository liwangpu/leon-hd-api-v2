using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    /// <summary>
    /// 产品规格IconUrl变更事件
    /// </summary>
    public class ProductSpecIconUpdatedEvent : INotification
    {
        public string ProductSpecId { get; protected set; }
        public string ProductId { get; protected set; }
        public string Icon { get; protected set; }

        public ProductSpecIconUpdatedEvent(string productId, string productSpecId, string icon)
        {
            ProductId = productId;
            ProductSpecId = productSpecId;
            Icon = icon;
        }
    }
}
