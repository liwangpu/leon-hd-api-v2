using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductSpecEntityTypeConfiguration : IEntityTypeConfiguration<ProductSpec>
    {
        public void Configure(EntityTypeBuilder<ProductSpec> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.Price);
            builder.Property(x => x.PartnerPrice);
            builder.Property(x => x.PurchasePrice);
            builder.Property(x => x.Icon);
            builder.Property(x => x.RelatedStaticMeshIds);
            builder.Metadata.FindNavigation(nameof(ProductSpec.Product)).SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
