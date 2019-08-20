using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.Maps
{
    public class MapPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<MapPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        #region ctor
        public MapPatchCommand()
        {

        }

        public MapPatchCommand(string id, JsonPatchDocument<MapPatchCommand> patchDoc)
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
