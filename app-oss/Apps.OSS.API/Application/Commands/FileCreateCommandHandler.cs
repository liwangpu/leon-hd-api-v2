using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Apps.OSS.API.Application.Commands
{
    public class FileCreateCommandHandler : IRequestHandler<FileCreateCommand, string>
    {
        public FileCreateCommandHandler()
        {

        }

        public Task<string> Handle(FileCreateCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
