using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.Products
{
    public class ProductPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<ProductPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }

        #region ctor
        public ProductPatchCommand()
        {

        }

        public ProductPatchCommand(string id, JsonPatchDocument<ProductPatchCommand> patchDoc)
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
