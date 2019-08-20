using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountBatchDeleteCommand : IRequest<ObjectResult>
    {
        public string Ids { get; set; }
    }
}
