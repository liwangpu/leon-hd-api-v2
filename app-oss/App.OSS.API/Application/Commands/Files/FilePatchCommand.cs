using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Newtonsoft.Json;

namespace App.OSS.API.Application.Commands.Files
{
    public class FilePatchCommand : IRequest
    {
        private readonly JsonPatchDocument<FilePatchCommand> patchDoc;

        /// <summary>
        /// Id
        /// </summary>
        [JsonIgnore]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int FileState { get; set; }

        public FilePatchCommand()
        {

        }

        public FilePatchCommand(string id, JsonPatchDocument<FilePatchCommand> patchDoc)
        {
            Id = id;
            this.patchDoc = patchDoc;
        }

        public void ApplyPatch()
        {
            patchDoc.ApplyTo(this);
        }
    }
}
