using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductPermissionItemEntityTypeConfiguration : IEntityTypeConfiguration<ProductPermissionItem>
    {
        public void Configure(EntityTypeBuilder<ProductPermissionItem> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(x => x.ProductId);
            builder.Metadata.FindNavigation(nameof(ProductPermissionItem.ProductPermissionGroup)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
