using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Textures
{
    public class TextureIdentityQueryHandler : IRequestHandler<TextureIdentityQuery, TextureIdentityQueryDTO>
    {
        private readonly ITextureRepository textureRepository;
        private readonly IStringLocalizer<CommonTranslation> localizer;

        public TextureIdentityQueryHandler(ITextureRepository textureRepository, IStringLocalizer<CommonTranslation> localizer)
        {
            this.textureRepository = textureRepository;
            this.localizer = localizer;
        }

        public async Task<TextureIdentityQueryDTO> Handle(TextureIdentityQuery request, CancellationToken cancellationToken)
        {
            var texture = await textureRepository.FindAsync(request.Id);

            if (texture == null)
                throw new HttpResourceNotFoundException(localizer["HttpRespond.NotFound", "Texture", request.Id]);



            return TextureIdentityQueryDTO.From(texture);
        }
    }
}
