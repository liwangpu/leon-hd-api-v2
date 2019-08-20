using App.Base.API;
using App.Basic.API.Application.Queries.Tokens;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace App.Basic.API.Application.Validations.Users
{
    public class TokenRequestValidator : AbstractValidator<TokenRequestQuery>
    {
        private readonly IAccountRepository accountRepository;

        public TokenRequestValidator(IStringLocalizer<CommonTranslation> localizer, IAccountRepository accountRepository)
        {
            this.accountRepository = accountRepository;
            RuleFor(cmd => cmd.Username).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Username"]);
            RuleFor(cmd => cmd.Password).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(localizer["FieldIsRequred", "Password"]);
            RuleFor(x => x).Must(x => CheckAccountExist(x.Username, x.Password)).WithMessage(x => localizer["LoginFailed"]);
        }

        public bool CheckAccountExist(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username) || string.IsNullOrWhiteSpace(password))
                return true;
            return accountRepository.Get(new TokenRequestSpecification(username, password)).Any();
        }
    }
}
