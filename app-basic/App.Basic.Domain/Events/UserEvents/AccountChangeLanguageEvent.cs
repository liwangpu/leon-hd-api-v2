using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    public class AccountChangeLanguageEvent : INotification
    {
        public Account Account { get; }

        public AccountChangeLanguageEvent(Account account)
        {
            Account = account;
        }
    }
}
