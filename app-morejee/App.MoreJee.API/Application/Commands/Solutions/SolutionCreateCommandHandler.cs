using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionCreateCommandHandler : IRequestHandler<SolutionCreateCommand, string>
    {
        private readonly ISolutionRepository solutionRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public SolutionCreateCommandHandler(ISolutionRepository solutionRepository, IIdentityService identityService)
        {
            this.solutionRepository = solutionRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<string> Handle(SolutionCreateCommand request, CancellationToken cancellationToken)
        {
            var data = new Solution(request.Name, request.Description, request.Icon, identityService.GetOrganizationId(), identityService.GetUserId());
            await solutionRepository.AddAsync(data);

            return data.Id;
        } 
        #endregion
    }
}
