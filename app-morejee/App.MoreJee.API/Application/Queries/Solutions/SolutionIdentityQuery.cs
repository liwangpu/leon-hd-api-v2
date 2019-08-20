using App.Base.Domain.Extentions;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using MediatR;

namespace App.MoreJee.API.Application.Queries.Solutions
{
    public class SolutionIdentityQuery : IRequest<SolutionIdentityQueryDTO>
    {
        public string Id { get; protected set; }

        public SolutionIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class SolutionIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }

        public static SolutionIdentityQueryDTO From(Solution data)
        {
            return new SolutionIdentityQueryDTO
            {
                Id=data.Id,
                Name = data.Name,
                Description = data.Description,
                Icon = data.Icon,
                CreatedTime = data.CreatedTime,
                ModifiedTime = data.ModifiedTime
            };
        }
    }
}
