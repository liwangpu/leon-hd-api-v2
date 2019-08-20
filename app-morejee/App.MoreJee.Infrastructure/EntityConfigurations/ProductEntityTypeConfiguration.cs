using App.MoreJee.Domain.AggregateModels.ProductAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class ProductEntityTypeConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.DefaultProductSpecId);
            builder.Property(x => x.Icon);
            builder.Property(x => x.MinPrice);
            builder.Property(x => x.MaxPrice);
            builder.Property(x => x.MinPartnerPrice);
            builder.Property(x => x.MaxPartnerPrice);
            builder.Property(x => x.MinPurchasePrice);
            builder.Property(x => x.MaxPurchasePrice);
            builder.Property(x => x.CategoryId);
            builder.Property(x => x.Brand);
            builder.Property(x => x.Unit);
            var navigation = builder.Metadata.FindNavigation(nameof(Product.OwnProductSpecs));
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.OwnProductSpecs).WithOne(s => s.Product).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
