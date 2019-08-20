using App.Base.API;
using App.Base.API.Infrastructure.Exceptions;
using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using MediatR;
using Microsoft.Extensions.Localization;
using System.Threading;
using System.Threading.Tasks;

namespace App.MoreJee.API.Application.Queries.Materials
{
    public class MaterialIdentityQueryHandler : IRequestHandler<MaterialIdentityQuery, MaterialIdentityQueryDTO>
    {
        private readonly IMaterialRepository materialRepository;
        private readonly IStringLocalizer<CommonTranslation> commonLocalizer;
        private readonly ICategoryRepository categoryRepository;

        #region ctor
        public MaterialIdentityQueryHandler(IMaterialRepository materialRepository, IStringLocalizer<CommonTranslation> commonLocalizer, ICategoryRepository categoryRepository)
        {
            this.materialRepository = materialRepository;
            this.commonLocalizer = commonLocalizer;
            this.categoryRepository = categoryRepository;
        }
        #endregion

        #region Handle
        public async Task<MaterialIdentityQueryDTO> Handle(MaterialIdentityQuery request, CancellationToken cancellationToken)
        {
            var data = await materialRepository.FindAsync(request.Id);
            if (data == null)
                throw new HttpResourceNotFoundException(commonLocalizer["HttpRespond.NotFound", "Material", request.Id]);

            var dto = MaterialIdentityQueryDTO.From(data);
            dto.CategoryName = await categoryRepository.GetCategoryName(data.CategoryId);
            return dto;
        }
        #endregion
    }
}
