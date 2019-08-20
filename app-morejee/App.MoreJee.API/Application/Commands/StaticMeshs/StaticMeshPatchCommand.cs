using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.StaticMeshs
{
    public class StaticMeshPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<StaticMeshPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        #region ctor
        public StaticMeshPatchCommand()
        {

        }

        public StaticMeshPatchCommand(string id, JsonPatchDocument<StaticMeshPatchCommand> patchDoc)
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
