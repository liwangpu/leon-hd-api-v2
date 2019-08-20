using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.MoreJee.API.Application.Commands.Maps
{
    public class MapBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
