using App.Base.API;
using App.OMS.API.Application.Commands.Customers;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace App.OMS.API.Application.Validations.Customers
{
    public class CustomerCreateValidator : AbstractValidator<CustomerCreateCommand>
    {
        public CustomerCreateValidator(IStringLocalizer<CommonTranslation> commonLocalizer)
        {
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.Phone).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Phone"]);
            RuleFor(cmd => cmd.Address).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Address"]);
        }
    }
}
