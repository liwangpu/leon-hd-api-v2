using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;
using MediatR;

namespace App.MoreJee.API.Application.Commands.Solutions
{
    public class SolutionPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<SolutionPatchCommand> patchDoc;
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        #region ctor
        public SolutionPatchCommand()
        {

        }

        public SolutionPatchCommand(string id, JsonPatchDocument<SolutionPatchCommand> patchDoc)
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
