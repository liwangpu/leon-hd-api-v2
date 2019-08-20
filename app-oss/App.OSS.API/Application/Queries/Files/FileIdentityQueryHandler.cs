using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.OSS.API.Application.Queries.Files
{
    public class FileIdentityQueryHandler : IRequestHandler<FileIdentityQuery, FileIdentityQueryDTO>
    {
        private readonly IFileAssetRepository fileAssetRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;
        private readonly IMapper mapper;

        public FileIdentityQueryHandler(IFileAssetRepository fileAssetRepository, IStringLocalizer<CommonTranslation> localizer, IMapper mapper)
        {
            this.fileAssetRepository = fileAssetRepository;
            this.localizer = localizer;
            this.mapper = mapper;
        }

        public async Task<FileIdentityQueryDTO> Handle(FileIdentityQuery request, CancellationToken cancellationToken)
        {
            var asset = await fileAssetRepository.FindAsync(request.Id);
            if (asset == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "File", request.Id]);
            return mapper.Map<FileIdentityQueryDTO>(asset);
        }
    }
}
