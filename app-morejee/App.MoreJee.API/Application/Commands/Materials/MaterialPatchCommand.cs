using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.Materials
{
    public class MaterialPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<MaterialPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }

        #region ctor
        public MaterialPatchCommand()
        {

        }

        public MaterialPatchCommand(string id, JsonPatchDocument<MaterialPatchCommand> patchDoc)
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
