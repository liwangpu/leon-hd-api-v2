using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;

namespace Apps.OSS.API.Application.Queries
{
    public class FileIdentityQueryHandler : IRequestHandler<FileIdentityQuery, FileIdentityQueryDTO>
    {
        public FileIdentityQueryHandler()
        {

        }

        public async Task<FileIdentityQueryDTO> Handle(FileIdentityQuery request, CancellationToken cancellationToken)
        {
            return new FileIdentityQueryDTO();
        }
    }
}
