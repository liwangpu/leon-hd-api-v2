using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.ProductAggregate
{
    public interface IProductSpecRepository : IRepository<ProductSpec>
    {
        Task LoadProductAsync(ProductSpec entity);
    }
}
