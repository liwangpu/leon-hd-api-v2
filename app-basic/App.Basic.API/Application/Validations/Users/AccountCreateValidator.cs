using App.Base.API;
using App.Basic.API.Application.Commands.Accounts;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace App.Basic.API.Application.Validations.Users
{
    public class AccountCreateValidator : AbstractValidator<AccountCreateCommand>
    {
        private readonly IAccountRepository accountRepository;

        public AccountCreateValidator(IStringLocalizer<CommonTranslation> localizer, IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;

            RuleFor(cmd => cmd.FistName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Fist Name"]);
            RuleFor(cmd => cmd.LastName).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Last Name"]);
            RuleFor(cmd => cmd.Mail).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Mail"]);
            RuleFor(x => x.Mail).Must(MustBeUniqueEmail).WithMessage(x => localizer["MailHasBeenUsed", x.Mail]);
        }

        public bool MustBeUniqueEmail(string mail)
        {
            //为空不校验
            if (string.IsNullOrWhiteSpace(mail))
                return true;

            return !accountRepository.Get(new AccountUniqueEmailCheckSpecification(mail)).Any();
        }
    }
}
