
using App.Basic.Domain.AggregateModels.PermissionAggregate;
using App.Basic.Domain.AggregateModels.UserAggregate;
using App.Basic.Domain.SeedWork;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace App.Basic.API.Application.Queries.AccessPoints
{
    public class CheckExistAssetPointQueryHandler : IRequestHandler<CheckExistAssetPointQuery, bool>
    {
        private readonly IAccountRepository accountRepository;
        private readonly ICustomRoleRepository customRoleRepository;

        #region ctor
        public CheckExistAssetPointQueryHandler(IAccountRepository accountRepository, ICustomRoleRepository customRoleRepository)
        {
            this.accountRepository = accountRepository;
            this.customRoleRepository = customRoleRepository;
        }
        #endregion

        #region Handle
        public async Task<bool> Handle(CheckExistAssetPointQuery request, CancellationToken cancellationToken)
        {
            var checkPointKey = request.PointKey.ToLower().Trim();
            var user = await accountRepository.FindAsync(request.UserId);
      
            //校验系统角色
            var sysRole = Enumeration.FromValue<SystemRole>(user.SystemRoleId);
            if (!string.IsNullOrWhiteSpace(sysRole.AccessPointKeys))
            {
                var pointsArr = sysRole.AccessPointKeys.Split(",", StringSplitOptions.RemoveEmptyEntries);
                var exist = pointsArr.Any(x => x.ToLower().Trim() == checkPointKey);
                if (exist)
                    return true;
            }

            await accountRepository.LoadOwnRolesAsync(user);

            foreach (var cus in user.OwnRoles)
            {
                var role = await customRoleRepository.FindAsync(cus.CustomRoleId);
                if (!string.IsNullOrWhiteSpace(role.AccessPointKeys))
                {
                    var pointsArr = role.AccessPointKeys.Split(",", StringSplitOptions.RemoveEmptyEntries);
                    var exist = pointsArr.Any(x => x.ToLower().Trim() == checkPointKey);
                    if (exist)
                        return true;
                }
            }


            return false;
        }
        #endregion
    }
}
