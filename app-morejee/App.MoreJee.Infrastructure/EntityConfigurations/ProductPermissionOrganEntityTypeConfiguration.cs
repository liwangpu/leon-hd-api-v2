using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductPermissionOrganEntityTypeConfiguration : IEntityTypeConfiguration<ProductPermissionOrgan>
    {
        public void Configure(EntityTypeBuilder<ProductPermissionOrgan> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(x => x.OrganizationId);
            builder.Metadata.FindNavigation(nameof(ProductPermissionOrgan.ProductPermissionGroup)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
