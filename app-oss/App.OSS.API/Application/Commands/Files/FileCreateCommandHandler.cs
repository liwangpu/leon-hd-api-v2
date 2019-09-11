using App.Base.API.Infrastructure.Services;
using App.OSS.API.Infrastructure.Consts;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.OSS.API.Application.Commands.Files
{
    public class FileCreateCommandHandler : IRequestHandler<FileCreateCommand, string>
    {
        private readonly IFileAssetRepository fileAssetRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public FileCreateCommandHandler(IFileAssetRepository fileAssetRepository, IIdentityService identityService)
        {
            this.fileAssetRepository = fileAssetRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(FileCreateCommand request, CancellationToken cancellationToken)
        {
            var file = await fileAssetRepository.FindAsync(request.MD5);
            if (file != null)
            {
                file.UpdateBasicInfo(request.Name, request.Description, request.FileState, identityService.GetUserId());
                fileAssetRepository.Update(file);
                await fileAssetRepository.UnitOfWork.SaveEntitiesAsync();
                return file.Id;
            }

            var url = $"/{OSSConst.AppRouteArea}/{OSSConst.ClientAssetFolder}/{request.MD5}.{request.FileExt}";
            file = new FileAsset(request.Name, request.Description, request.FileExt, request.FileState, request.Size, url, identityService.GetUserId());
            file._CustomizeId(request.MD5);
            fileAssetRepository.Add(file);
            await fileAssetRepository.UnitOfWork.SaveEntitiesAsync();
            return file.Id;
        } 
        #endregion
    }
}
