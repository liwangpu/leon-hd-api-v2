using MediatR;

namespace App.MoreJee.Domain.Events.ProductEvents
{
    public class DeleteProductSpecRelatedStaticMeshEvent : INotification
    {
        public string StaticMeshIds { get; set; }
        public DeleteProductSpecRelatedStaticMeshEvent(string ids)
        {
            StaticMeshIds = ids;
        }
    }
}
