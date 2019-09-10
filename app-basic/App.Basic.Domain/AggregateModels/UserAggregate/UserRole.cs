using App.Basic.Domain.SeedWork;

namespace App.Basic.Domain.AggregateModels.UserAggregate
{
    public class UserRole : Entity, IAggregateRoot
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
            AccountId = accountId;
            CustomRoleId = roleId;
        }
        #endregion


    }
}
