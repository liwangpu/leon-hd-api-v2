using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.Basic.API.Application.Commands.AccessPoints
{
    public class AccessPointPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<AccessPointPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string PointKey { get; set; }

        #region ctor
        public AccessPointPatchCommand()
        {

        }

        public AccessPointPatchCommand(string id, JsonPatchDocument<AccessPointPatchCommand> patchDoc)
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
