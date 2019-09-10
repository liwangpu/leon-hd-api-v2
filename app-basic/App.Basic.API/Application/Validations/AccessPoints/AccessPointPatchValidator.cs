using App.Base.API;
using App.Basic.API.Application.Commands.AccessPoints;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.Consts;
using App.Basic.Infrastructure.Specifications.AccessPointSpecifications;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Validations.AccessPoints
{
    public class AccessPointPatchValidator : AbstractValidator<AccessPointPatchCommand>
    {
        private readonly IAccessPointRepository accessPointRepository;

        public AccessPointPatchValidator(IStringLocalizer<CommonTranslation> localizer, IAccessPointRepository accessPointRepository)
        {
            this.accessPointRepository = accessPointRepository;

            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.PointKey).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "PointKey"]);
            RuleFor(cmd => cmd).Must(x => UniquePointKey(x.Id, x.PointKey)).WithMessage(x => localizer["FieldValueIsDuplicate", x.PointKey, "PointKey"]);
            RuleFor(cmd => cmd.Id).MustAsync(async (id, token) => await ExitRecord(id)).WithMessage(cmd => localizer["HttpRespond.NotFound", "AccessPoint", cmd.Id]);
            RuleFor(cmd => cmd.Id).MustAsync(async (id, token) => await NotInnerValue(id)).WithMessage(cmd => localizer["CannotUpdateInnerValue"]);
        }

        public async Task<bool> ExitRecord(string id)
        {
            var data = await accessPointRepository.FindAsync(id);
            return data != null;
        }

        public async Task<bool> NotInnerValue(string id)
        {
            var data = await accessPointRepository.FindAsync(id);
            //实体不存在不校验
            if (data == null) return true;

            return data.IsInner == EntityStateConst.No;
        }

        public bool UniquePointKey(string id, string key)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(key))
                return true;

            var existKey = accessPointRepository.Get(new PointKeyUniqueCheckSpecification(key, id)).Any();
            return !existKey;
        }

    }
}
