using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Infrastructure.Specifications.UserRoleSpecifications;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.UserRoles
{
    public class UserRoleQueryHandler : IRequestHandler<UserRoleQuery, List<UserRolePagingQueryDTO>>
    {
        private readonly IUserRoleRepository userRoleRepository;

        public UserRoleQueryHandler(IUserRoleRepository userRoleRepository)
        {
            this.userRoleRepository = userRoleRepository;
        }

        public async Task<List<UserRolePagingQueryDTO>> Handle(UserRoleQuery request, CancellationToken cancellationToken)
        {
            var dtos = await userRoleRepository.Get(new GetUserRoleByAccountIdSpecification(request.AccountId)).Select(x => UserRolePagingQueryDTO.From(x)).ToListAsync();
            return dtos;
        }
    }
}
