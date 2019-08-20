using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TextureBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
