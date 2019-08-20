using System;
using System.Collections.Generic;
using System.Text;

namespace App.MoreJee.Export.Models
{
    public class ProductBriefIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string Brand { get; set; }
        public string Unit { get; set; }
    }

    public class ProductSpecBriefIdentityQueryDTO
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Icon { get; set; }
        public string Description { get; set; }
        public string ProductId { get; set; }
    }
}
