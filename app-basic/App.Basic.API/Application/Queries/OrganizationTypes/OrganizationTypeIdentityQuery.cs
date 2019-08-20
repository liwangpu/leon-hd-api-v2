using MediatR;

namespace App.Basic.API.Application.Queries.OrganizationTypes
{
    public class OrganizationTypeIdentityQuery : IRequest<OrganizationTypeIdentityQueryDTO>
    {
        public int Id { get; set; }

        public OrganizationTypeIdentityQuery(int id)
        {
            Id = id;
        }
    }

    public class OrganizationTypeIdentityQueryDTO
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        public string Name { get; set; }
        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}
