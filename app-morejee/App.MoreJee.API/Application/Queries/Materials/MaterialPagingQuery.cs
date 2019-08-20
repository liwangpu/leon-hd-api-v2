using App.Base.API.Application.Queries;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Materials
{
    public class MaterialPagingQuery : PagingQueryRequest, IRequest<PagingQueryResult<MaterialPagingQueryDTO>>
    {
        public string CategoryId { get; set; }
        public bool UnClassified { get; set; }
    }

    public class MaterialPagingQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public static MaterialPagingQueryDTO From(string id, string name, string description, string icon, string categoryId, long createdTime, long modifiedTime)
        {
            return new MaterialPagingQueryDTO
            {
                Id = id,
                Name = name,
                Description = description,
                Icon = icon,
                CategoryId = categoryId,
                CreatedTime = createdTime,
                ModifiedTime = modifiedTime
            };
        }
    }
}
