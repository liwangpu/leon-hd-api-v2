using MediatR;
namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountDeleteCommand : IRequest
    {
        public string Id { get; set; }
    }
}
