using MediatR;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class ResetPasswordCommand : IRequest
    {
        public string AccountId { get; set; }
        public string Password { get; set; }
    }
}
