using App.Base.API.Application.Queries;
using App.Base.API.Infrastructure.Services;
using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using App.MoreJee.Infrastructure.Specifications.SolutionSpecifications;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
namespace App.MoreJee.API.Application.Queries.Solutions
{
    public class SolutionPagingQueryHandler : IRequestHandler<SolutionPagingQuery, PagingQueryResult<SolutionPagingQueryDTO>>
    {
        private readonly ISolutionRepository solutionRepository;
        private readonly IMapper mapper;
        private readonly IIdentityService identityService;

        #region ctor
        public SolutionPagingQueryHandler(ISolutionRepository solutionRepository, IMapper mapper, IIdentityService identityService)
        {
            this.solutionRepository = solutionRepository;
            this.mapper = mapper;
            this.identityService = identityService;
        }
        #endregion

        #region Handle
        public async Task<PagingQueryResult<SolutionPagingQueryDTO>> Handle(SolutionPagingQuery request, CancellationToken cancellationToken)
        {
            request.CheckPagingParam();
            var result = new PagingQueryResult<SolutionPagingQueryDTO>();

            var specification = new SolutionPagingSpecification(identityService.GetUserId(), request.Page, request.PageSize, request.Search, request.OrderBy, request.Desc);
            result.Total = await solutionRepository.Get(specification).CountAsync();
            result.Data = await solutionRepository.Paging(specification).Select(x => mapper.Map<SolutionPagingQueryDTO>(x)).ToListAsync();
            return result;
        } 
        #endregion
    }
}
