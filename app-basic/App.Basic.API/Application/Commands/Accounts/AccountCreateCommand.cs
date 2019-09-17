using MediatR;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountCreateCommand : IRequest<string>
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Password { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }
    }

}
