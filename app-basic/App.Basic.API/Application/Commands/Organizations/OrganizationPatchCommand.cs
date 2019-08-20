using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace App.Basic.API.Application.Commands.Organizations
{
    public class OrganizationPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<OrganizationPatchCommand> patchDoc;

        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #region ctor
        public OrganizationPatchCommand()
        {

        }

        public OrganizationPatchCommand(string id, JsonPatchDocument<OrganizationPatchCommand> patchDoc)
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
