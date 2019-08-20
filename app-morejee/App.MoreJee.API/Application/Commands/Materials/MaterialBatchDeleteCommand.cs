using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
