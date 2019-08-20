using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductPermissionGroupEntityTypeConfiguration : IEntityTypeConfiguration<ProductPermissionGroup>
    {
        public void Configure(EntityTypeBuilder<ProductPermissionGroup> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.OrganizationId);
            var productNav = builder.Metadata.FindNavigation(nameof(ProductPermissionGroup.OwnProductItems));
            productNav.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.OwnProductItems).WithOne(s => s.ProductPermissionGroup).OnDelete(DeleteBehavior.Cascade);

            var organNav = builder.Metadata.FindNavigation(nameof(ProductPermissionGroup.OwnOrganItems));
            productNav.SetPropertyAccessMode(PropertyAccessMode.Field);
            builder.HasMany(x => x.OwnOrganItems).WithOne(s => s.ProductPermissionGroup).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
