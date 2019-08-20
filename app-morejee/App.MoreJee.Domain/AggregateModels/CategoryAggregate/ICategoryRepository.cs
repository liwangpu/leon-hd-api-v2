using App.Base.Domain.Common;
using System.Threading.Tasks;

namespace App.MoreJee.Domain.AggregateModels.CategoryAggregate
{
    public interface ICategoryRepository : IRepository<Category>
    {
        Task ChangeHierarchyAsync(string categoryId, string parentId);
        Task<string> GetCategoryName(string categoryId);
        Task<string> GetAllSubCategoryIds(string categoryId);
    }
}
