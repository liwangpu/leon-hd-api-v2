using App.Base.API;
using App.Base.API.Infrastructure.Services;
using App.Basic.API.Application.Commands.Organizations;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using App.Basic.Infrastructure.Specifications.AccountSpecifications;
using FluentValidation;
using Microsoft.Extensions.Localization;
using System.Linq;

namespace App.Basic.API.Application.Validations.Users
{
    public class OrganizationCreateValidator : AbstractValidator<OrganizationCreateCommand>
    {
        private readonly IAccountRepository accountRepository;
        private readonly IIdentityService identityService;

        public OrganizationCreateValidator(IAccountRepository accountRepository, IIdentityService identityService, IStringLocalizer<CommonTranslation> commonLocalizer, IStringLocalizer<AppBasicTranslation> basicLocalizer)
        {
            this.accountRepository = accountRepository;
            this.identityService = identityService;
            RuleFor(cmd => cmd.Name).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Name"]);
            RuleFor(cmd => cmd.Phone).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Phone"]);
            RuleFor(cmd => cmd.Mail).Must(x => !string.IsNullOrWhiteSpace(x)).WithMessage(commonLocalizer["FieldIsRequred", "Mail"]);
            RuleFor(x => x.Mail).Must(MustBeUniqueEmail).WithMessage(x => commonLocalizer["MailHasBeenUsed", x.Mail]);
            RuleFor(x => x.OrganizationTypeId).Must(MustValidOrganizationType).WithMessage(x => basicLocalizer["OrganizationType.NotApplicable"]);
        }

        public bool MustBeUniqueEmail(string mail)
        {
            if (string.IsNullOrWhiteSpace(mail))
                return true;

            return !accountRepository.Get(new AccountUniqueEmailCheckSpecification(mail)).Any();
        }

        public bool MustValidOrganizationType(int organTypeId)
        {
            var types = Enumeration.GetAll<OrganizationType>();
            if (!types.Any(x => x.Id == organTypeId))
                return false;

            var currentUserOrganTypeId = identityService.GetOrganizationTypeId();
            //服务商创建品牌商
            if (currentUserOrganTypeId == OrganizationType.ServiceProvider.Id.ToString() && organTypeId == OrganizationType.Brand.Id)
                return true;
            //品牌商创建代理商和供应商
            if (currentUserOrganTypeId == OrganizationType.Brand.Id.ToString() && (organTypeId == OrganizationType.Partner.Id || organTypeId == OrganizationType.Supplier.Id))
                return true;

            return false;
        }
    }
}
