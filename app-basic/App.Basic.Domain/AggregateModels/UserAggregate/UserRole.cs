using App.Base.Domain.Common;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public class UserRole : Entity
    {
        public string CustomRoleId { get; protected set; }
        public string AccountId { get; protected set; }
        public Account Account { get; protected set; }

        #region ctor
        protected UserRole()
        {

        }

        public UserRole(string accountId, string roleId)
            : this()
        {
            Id = GuidGen.NewGUID();
            AccountId = accountId;
            CustomRoleId = roleId;
        }
        #endregion


    }
}
