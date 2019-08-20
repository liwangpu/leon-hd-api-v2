using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Solutions
{
    public class SolutionIdentityQueryHandler : IRequestHandler<SolutionIdentityQuery, SolutionIdentityQueryDTO>
    {
        private readonly ISolutionRepository solutionRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;

        #region ctor
        public SolutionIdentityQueryHandler(ISolutionRepository solutionRepository, IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            this.solutionRepository = solutionRepository;
            this.commonLocalizer = commonLocalizer;
        }
        #endregion

        #region Handle
        public async Task<SolutionIdentityQueryDTO> Handle(SolutionIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await solutionRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Solution", request.Id]);

            return SolutionIdentityQueryDTO.From(data);
        }
        #endregion
    }
}
