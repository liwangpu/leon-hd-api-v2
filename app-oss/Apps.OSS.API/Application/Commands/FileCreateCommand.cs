using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace Apps.OSS.API.Application.Commands
{
    public class FileCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
    }
}
