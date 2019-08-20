using App.Base.API.Application.Queries;
using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using App.OSS.Infrastructure.Specifications;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.OSS.API.Application.Queries.Files
{
    public class FilePagingQueryHandler : IRequestHandler<FilePagingQuery, PagingQueryResult<FilePagingQueryDTO>>
    {
        private readonly IFileAssetRepository fileAssetRepository;
        private readonly IMapper mapper;

        public FilePagingQueryHandler(IFileAssetRepository fileAssetRepository, IMapper mapper)
        {
            this.fileAssetRepository = fileAssetRepository;
            this.mapper = mapper;
        }

        public async Task<PagingQueryResult<FilePagingQueryDTO>> Handle(FilePagingQuery request, CancellationToken cancellationToken)
        {
            request.CheckPagingParam();
            var result = new PagingQueryResult<FilePagingQueryDTO>();
            var specification = new FilePagingSpecification(request.Page, request.PageSize, request.Search, request.OrderBy, request.Desc);
            result.Total = await fileAssetRepository.Get(specification).CountAsync();
            result.Data = await fileAssetRepository.Paging(specification).Select(x => mapper.Map<FilePagingQueryDTO>(x)).ToListAsync();
            return result;
        }
    }
}
