using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
