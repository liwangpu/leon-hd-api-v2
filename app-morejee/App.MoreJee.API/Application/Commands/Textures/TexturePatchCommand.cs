using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.Textures
{
    public class TexturePatchCommand : IRequest
    {
        private readonly JsonPatchDocument<TexturePatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }

        #region ctor
        public TexturePatchCommand()
        {

        }

        public TexturePatchCommand(string id, JsonPatchDocument<TexturePatchCommand> patchDoc)
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
