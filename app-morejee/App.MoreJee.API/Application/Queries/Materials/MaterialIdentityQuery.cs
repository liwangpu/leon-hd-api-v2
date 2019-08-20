using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Materials
{
    public class MaterialIdentityQuery : IRequest<MaterialIdentityQueryDTO>
    {
        public string Id { get; protected set; }

        public MaterialIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class MaterialIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }

        public static MaterialIdentityQueryDTO From(Material data)
        {
            return new MaterialIdentityQueryDTO
            {
                Id = data.Id,
                Name = data.Name,
                Description = data.Description,
                Icon = data.Icon,
                CategoryId = data.CategoryId,
                Creator = data.Creator,
                Modifier = data.Modifier,
                CreatedTime = data.CreatedTime,
                ModifiedTime = data.ModifiedTime
            };
        }

    }
}
