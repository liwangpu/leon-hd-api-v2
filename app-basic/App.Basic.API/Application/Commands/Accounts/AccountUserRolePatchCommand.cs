using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Basic.API.Application.Commands.Accounts
{
    public class AccountUserRolePatchCommand : IRequest
    {

        private readonly JsonPatchDocument<AccountUserRolePatchCommand> patchDoc;

        public string Id { get; set; }
        public string CustomRoleIds { get; set; }

        #region ctor
        public AccountUserRolePatchCommand()
        {

        }

        public AccountUserRolePatchCommand(string id, JsonPatchDocument<AccountUserRolePatchCommand> patchDoc)
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
