using MediatR;
using System;

namespace App.MoreJee.API.Application.Queries.Categories
{
    public class CategoryIdentityQuery : IRequest<CategoryIdentityQueryDTO>
    {
        public string Id { get; protected set; }

        public CategoryIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class CategoryIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string NodeType { get; set; }
        public string Resource { get; set; }
        public string ParentId { get; set; }
        public int DisplayIndex { get; set; }
        public int LValue { get; set; }
        public int RValue { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public DateTime CreatedTime { get; set; }
        public DateTime ModifiedTime { get; set; }
        public string OrganizationId { get; set; }
    }
}
