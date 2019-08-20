using App.Base.API;
using App.MoreJee.API.Application.Commands.Solutions;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace App.MoreJee.API.Application.Validations.Solutions
{
    public class SolutionCreateValidator : AbstractValidator<SolutionCreateCommand>
    {
        public SolutionCreateValidator(IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
        }
    }
}
