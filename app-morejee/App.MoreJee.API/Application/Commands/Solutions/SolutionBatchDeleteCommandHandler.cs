using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionBatchDeleteCommandHandler : IRequestHandler<SolutionBatchDeleteCommand>
    {
        private readonly ISolutionRepository solutionRepository;
        private readonly IIdentityService identityService;
        #region ctor
        public SolutionBatchDeleteCommandHandler(ISolutionRepository solutionRepository, IIdentityService identityService)
        {
            this.solutionRepository = solutionRepository;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<Unit> Handle(SolutionBatchDeleteCommand request, CancellationToken cancellationToken)
        {
            var operatorId = identityService.GetUserId();
            var idArr = request.Ids.Split(",", StringSplitOptions.RemoveEmptyEntries);
            for (int i = 0, len = idArr.Count(); i < len; i++)
            {
                var id = idArr[i];

                var data = await solutionRepository.FindAsync(id);
                if (data == null)
                    continue;
                

                await solutionRepository.DeleteAsync(id, operatorId);
            }

            return Unit.Value;
        }
        #endregion
    }
}
