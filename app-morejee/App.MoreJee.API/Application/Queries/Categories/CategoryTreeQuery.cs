using App.Base.Domain.Extentions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using MediatR;
using System.Collections.Generic;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryTreeQuery : IRequest<CategoryTreeQueryDTO>
    {
        public string Id { get; protected set; }

        public string FilteredIds { get; protected set; }
        public CategoryTreeQuery(string id, string filteredIds)
        {
            Id = id;
            FilteredIds = filteredIds;
        }
    }


    public class CategoryTreeQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string NodeType { get; set; }
        public string ParentId { get; set; }
        public int LValue { get; set; }
        public int RValue { get; set; }
        public int DisplayIndex { get; set; }
        public bool FirstNode { get; set; }
        public bool LastNode { get; set; }
        public string Resource { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }

        public List<CategoryTreeQueryDTO> Children = new List<CategoryTreeQueryDTO>();

        public static CategoryTreeQueryDTO From(Category cat)
        {
            return new CategoryTreeQueryDTO
            {
                Id = cat.Id,
                Name = cat.Name,
                Description = cat.Description,
                Icon = cat.Icon,
                LValue = cat.LValue,
                RValue = cat.RValue,
                NodeType = cat.NodeType,
                ParentId = cat.ParentId,
                DisplayIndex = cat.DisplayIndex,
                Resource = cat.Resource,
                CreatedTime = cat.CreatedTime.ToUnixTimeSeconds(),
                ModifiedTime = cat.ModifiedTime.ToUnixTimeSeconds()
            };
        }
    }
}
