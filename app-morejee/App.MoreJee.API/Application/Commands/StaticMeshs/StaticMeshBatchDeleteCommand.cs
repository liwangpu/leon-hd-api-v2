using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.MoreJee.API.Application.Commands.StaticMeshs
{
    public class StaticMeshBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
