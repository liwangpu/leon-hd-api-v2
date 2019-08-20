using MediatR;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.JsonPatch.Operations;

namespace App.MoreJee.API.Application.Commands.ProductSpecs
{
    public class ProductSpecPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<ProductSpecPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public decimal PartnerPrice { get; set; }
        public decimal PurchasePrice { get; set; }
        public string Icon { get; set; }

        #region ctor
        public ProductSpecPatchCommand()
        {

        }

        public ProductSpecPatchCommand(string id, JsonPatchDocument<ProductSpecPatchCommand> patchDoc)
        {
            Id = id;
            this.patchDoc = patchDoc;
        }
        #endregion

        public void ApplyPatch()
        {
            patchDoc.ApplyTo(this);
        }

        public void DisablePriceChange()
        {
            var it = patchDoc.Operations.Find(x => x.path.ToLower() == "/price" || x.path.ToLower() == "price");
            if (it != null)
                patchDoc.Operations.Remove(it);
        }

        public void DisablePartnerPriceChange()
        {
            var it = patchDoc.Operations.Find(x => x.path.ToLower() == "/partnerprice" || x.path.ToLower() == "partnerprice");
            if (it != null)
                patchDoc.Operations.Remove(it);
        }

        public void DisablePurchasePriceChange()
        {
            var it = patchDoc.Operations.Find(x => x.path.ToLower() == "/purchaseprice" || x.path.ToLower() == "purchaseprice");
            if (it != null)
                patchDoc.Operations.Remove(it);
        }

    }
}
