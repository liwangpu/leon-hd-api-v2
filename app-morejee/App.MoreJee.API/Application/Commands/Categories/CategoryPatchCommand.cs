using MediatR;
using Microsoft.AspNetCore.JsonPatch;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryPatchCommand : IRequest
    {
        private readonly JsonPatchDocument<CategoryPatchCommand> patchDoc;

        public string Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string NodeType { get; set; }
        public string Resource { get; set; }
        public string ParentId { get; set; }
        public int DisplayIndex { get; set; }

        #region ctor
        public CategoryPatchCommand()
        {

        }

        public CategoryPatchCommand(string id, JsonPatchDocument<CategoryPatchCommand> patchDoc)
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
