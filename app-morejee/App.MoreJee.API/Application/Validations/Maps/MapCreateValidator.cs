using App.Base.API;
using App.MoreJee.API.Application.Commands.Maps;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace App.MoreJee.API.Application.Validations.Maps
{
    public class MapCreateValidator : AbstractValidator<MapCreateCommand>
    {
        public MapCreateValidator(IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
        }

    }
}
