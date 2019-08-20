using MediatR;
using System;

namespace App.MoreJee.API.Application.Queries.StaticMeshs
{
    public class StaticMeshIdentityQuery : IRequest<StaticMeshIdentityQueryDTO>
    {
        public string Id { get; protected set; }

        public StaticMeshIdentityQuery(string id)
        {
            Id = id;
        }
    }

    public class StaticMeshIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Creator { get; set; }
        public string Modifier { get; set; }
        public long CreatedTime { get; set; }
        public long ModifiedTime { get; set; }
        public string OrganizationId { get; set; }
        public string RelatedProductSpecIds { get; set; }

    }
}
