using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Maps
{
    public class MapIdentityQueryHandler : IRequestHandler<MapIdentityQuery, MapIdentityQueryDTO>
    {
        private readonly IMapRepository mapRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public MapIdentityQueryHandler(IMapRepository mapRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.mapRepository = mapRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<MapIdentityQueryDTO> Handle(MapIdentityQuery request, CancellationToken cancellationToken)
        {
            var map = await mapRepository.FindAsync(request.Id);
            if (map == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Map", request.Id]);

            return MapIdentityQueryDTO.From(map);
        }
        #endregion
    }
}
