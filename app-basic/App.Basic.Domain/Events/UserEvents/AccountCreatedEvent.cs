using App.Basic.Domain.AggregateModels.UserAggregate;
using MediatR;

namespace App.Basic.Domain.Events.UserEvents
{
    public class AccountCreatedEvent : INotification
    {
        public Account Account { get; }

        public AccountCreatedEvent(Account account)
        {
            Account = account;
        }

    }
}
