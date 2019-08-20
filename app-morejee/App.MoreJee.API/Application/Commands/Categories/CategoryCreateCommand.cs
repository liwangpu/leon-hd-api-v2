using App.MoreJee.Domain.Consts;
using MediatR;

namespace App.MoreJee.API.Application.Commands.Categories
{
    public class CategoryCreateCommand : IRequest<string>
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string ParentId { get; set; }
        public string NodeType { get; set; }
        public string Resource { get; set; }

        //简单修正一下分类资源类型
        public void CheckResource()
        {
            if (!string.IsNullOrWhiteSpace(Resource))
            {
                var res = Resource.ToLower().Trim();
                if (res == MoreJeeConst.CategoryResource_Product)
                    Resource = MoreJeeConst.CategoryResource_Product;
                else if (res == MoreJeeConst.CategoryResource_Material)
                    Resource = MoreJeeConst.CategoryResource_Material;
                else if (res == MoreJeeConst.CategoryResource_ProductGroup)
                    Resource = MoreJeeConst.CategoryResource_ProductGroup;
                else
                    Resource = res;
            }
        }
    }
}
