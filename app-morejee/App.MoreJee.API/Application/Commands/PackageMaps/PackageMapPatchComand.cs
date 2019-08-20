using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.PackageMaps
{
    public class PackageMapPatchComand : IRequest
    {
        private readonly JsonPatchDocument<PackageMapPatchComand> patchDoc;

        public string Id { get; set; }
        public string ResourceId { get; set; }
        public string ResourceType { get; set; }

        #region ctor
        public PackageMapPatchComand()
        {

        }

        public PackageMapPatchComand(string id, JsonPatchDocument<PackageMapPatchComand> patchDoc)
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
