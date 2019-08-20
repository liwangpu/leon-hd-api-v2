using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.ProductPermissionGroups
{
    public class ProductPermissionGroupPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<ProductPermissionGroupPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        #region ctor
        public ProductPermissionGroupPatchCommand()
        {

        }

        public ProductPermissionGroupPatchCommand(string id, JsonPatchDocument<ProductPermissionGroupPatchCommand> patchDoc)
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
