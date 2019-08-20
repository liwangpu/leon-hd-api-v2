using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Basic.API.Application.Commands.CustomRoles
{
    public class CustomRoleBatchCommand : IRequest
    {
        private readonly JsonPatchDocument<CustomRoleBatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string AccessPointKeys { get; set; }
        #region ctor
        public CustomRoleBatchCommand()
        {

        }

        public CustomRoleBatchCommand(string id, JsonPatchDocument<CustomRoleBatchCommand> patchDoc)
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
