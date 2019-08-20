using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
