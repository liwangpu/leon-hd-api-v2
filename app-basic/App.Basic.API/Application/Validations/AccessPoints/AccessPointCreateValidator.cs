using App.Base.API;
using App.Basic.API.Application.Commands.AccessPoints;
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Infrastructure.Specifications.AccessPointSpecifications;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace App.Basic.API.Application.Validations.AccessPoints
{
    public class AccessPointCreateValidator : AbstractValidator<AccessPointCreateCommand>
    {
        private readonly IAccessPointRepository accessPointRepository;

        public AccessPointCreateValidator(IStringLocalizer<CommonTranslation> localizer, IAccessPointRepository accessPointRepository)
        {
            this.accessPointRepository = accessPointRepository;
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.PointKey).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "PointKey"]);
            RuleFor(cmd => cmd).Must(x => UniquePointKey(x.PointKey)).WithMessage(x => localizer["FieldValueIsDuplicate", x.PointKey, "PointKey"]);
        }

        public bool UniquePointKey(string key)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(key))
                return true;

            var existKey = accessPointRepository.Get(new PointKeyUniqueCheckSpecification(key)).Any();
            return !existKey;
        }
    }
}
