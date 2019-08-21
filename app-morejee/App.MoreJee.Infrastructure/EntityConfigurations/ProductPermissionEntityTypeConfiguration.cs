using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductPermissionEntityTypeConfiguration : IEntityTypeConfiguration<ProductPermission>
    {
        public void Configure(EntityTypeBuilder<ProductPermission> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.ProductPermissionGroupId);

            builder.HasIndex(x => x.ProductId);
            builder.HasIndex(x => x.OrganizationId);
        }
    }
}
