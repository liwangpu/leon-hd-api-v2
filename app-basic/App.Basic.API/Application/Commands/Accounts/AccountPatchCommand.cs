using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<AccountPatchCommand> patchDoc;

        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Mail { get; set; }
        public string Phone { get; set; }

        #region ctor
        public AccountPatchCommand()
        {

        }

        public AccountPatchCommand(string id, JsonPatchDocument<AccountPatchCommand> patchDoc)
        {
            Id = id;
            this.patchDoc = patchDoc;
        }
        #endregion

        public void ApplyPatch()
        {
            patchDoc.ApplyTo(this);
        }
    }
}
