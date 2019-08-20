using App.Base.API.Infrastructure.Services;
using App.Base.Domain.Common;
using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationCreateCommandHandler : IRequestHandler<OrganizationCreateCommand, string>
    {
        private readonly IOrganizationRepository organRepository;
        private readonly IIdentityService identityService;

        #region ctor
        public OrganizationCreateCommandHandler(IOrganizationRepository organRepository, IIdentityService identityService)
        {
            this.organRepository = organRepository;
            this.identityService = identityService;
        }
        #endregion

        public async Task<string> Handle(OrganizationCreateCommand request, CancellationToken cancellationToken)
        {
            var organ = new Organization(Enumeration.FromValue<OrganizationType>(request.OrganizationTypeId), request.Name, request.Description, request.Mail, request.Phone, identityService.GetUserId(), identityService.GetOrganizationId());
            await organRepository.AddAsync(organ);
            return organ.Id;
        }
    }
}
