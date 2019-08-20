using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using AutoMapper;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionPatchCommandHandler : IRequestHandler<SolutionPatchCommand>
    {
        private readonly ISolutionRepository solutionRepository;
        private readonly IIdentityService identityService;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly IMapper mapper;
        #region ctor
        public SolutionPatchCommandHandler(ISolutionRepository solutionRepository, IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer, IMapper mapper)
        {
            this.solutionRepository = solutionRepository;
            this.identityService = identityService;
            this.commonLocalizer = commonLocalizer;
            this.mapper = mapper;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(SolutionPatchCommand request, CancellationToken cancellationToken)
        {
            var product = await solutionRepository.FindAsync(request.Id);
            if (product == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Solution", request.Id]);

            mapper.Map(product, request);
            request.ApplyPatch();
            var modifier = identityService.GetUserId();
            product.UpdateBasicInfo(request.Name, request.Description, request.Icon, modifier);
            await solutionRepository.UpdateAsync(product);
            return Unit.Value;
        }
        #endregion
    }
}
