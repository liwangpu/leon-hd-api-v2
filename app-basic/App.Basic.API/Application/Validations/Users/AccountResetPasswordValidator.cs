using App.Base.API;
using App.Basic.API.Application.Commands.Accounts;
using FluentValidation;
using Microsoft.Extensions.Localization;

namespace App.Basic.API.Application.Validations.Users
{
    public class AccountResetPasswordValidator : AbstractValidator<ResetPasswordCommand>
    {

        public AccountResetPasswordValidator(IStringLocalizer<CommonTranslation> commanLocalizer)
        {
            RuleFor(cmd => cmd.AccountId).Must(MustBeManagedUser).WithMessage(commanLocalizer["OperateForbidden"]);
            RuleFor(cmd => cmd.AccountId).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commanLocalizer["FieldIsRequred", "AccountId"]);
            RuleFor(cmd => cmd.Password).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commanLocalizer["FieldIsRequred", "Password"]);      
        }

        public bool MustBeManagedUser(string accountId)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(accountId))
                return true;



            return true;
        }
    }
}
