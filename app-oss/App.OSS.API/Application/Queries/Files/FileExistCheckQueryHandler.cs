using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using App.OSS.Infrastructure.Specifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using System.Linq;


namespace App.OSS.API.Application.Queries.Files
{
    public class FileExistCheckQueryHandler : IRequestHandler<FileExistCheckQuery, string>
    {
        private readonly IFileAssetRepository fileAssetRepository;

        #region ctor
        public FileExistCheckQueryHandler(IFileAssetRepository fileAssetRepository)
        {
            this.fileAssetRepository = fileAssetRepository;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(FileExistCheckQuery request, CancellationToken cancellationToken)
        {
            var url = await fileAssetRepository.Get(new CheckFileExistSpecification(request.Md5)).Select(x => x.Url).FirstOrDefaultAsync();

            return url;
        }
        #endregion
    }
}
