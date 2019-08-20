using MediatR;
using System.Collections.Generic;

namespace App.Basic.API.Application.Queries.Organizations
{
    public class OrganizationBriefIdentitiesQuery : IRequest<List<OrganizationBriefIdentitiesQueryDTO>>
    {
        public string Ids { get; set; }
    }

    public class OrganizationBriefIdentitiesQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public static OrganizationBriefIdentitiesQueryDTO From(string id, string name, string description)
        {
            return new OrganizationBriefIdentitiesQueryDTO
            {
                Id = id,
                Name = name,
                Description = description
            };
        }
    }
}
