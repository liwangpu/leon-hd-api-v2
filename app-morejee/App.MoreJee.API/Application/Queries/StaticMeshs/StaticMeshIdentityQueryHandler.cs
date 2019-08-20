using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.StaticMeshs
{
    public class StaticMeshIdentityQueryHandler : IRequestHandler<StaticMeshIdentityQuery, StaticMeshIdentityQueryDTO>
    {
        private readonly IStaticMeshRepository staticMeshRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;

        #region ctor
        public StaticMeshIdentityQueryHandler(IStaticMeshRepository staticMeshRepository, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper)
        {
            this.staticMeshRepository = staticMeshRepository;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<StaticMeshIdentityQueryDTO> Handle(StaticMeshIdentityQuery request, CancellationToken cancellationToken)
        {
            var staticMesh = await staticMeshRepository.FindAsync(request.Id);
            if (staticMesh == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "StaticMesh", request.Id]);

            return mapper.Map<StaticMeshIdentityQueryDTO>(staticMesh);
        }
        #endregion
    }
}
