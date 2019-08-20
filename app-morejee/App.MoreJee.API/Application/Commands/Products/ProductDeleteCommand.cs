using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductDeleteCommand : IRequest
    {
        public string Id { get; protected set; }

        public ProductDeleteCommand(string id)
        {
            Id = id;
        }
    }
}
