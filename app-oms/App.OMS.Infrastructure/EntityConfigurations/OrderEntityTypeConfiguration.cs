using App.OMS.Domain.AggregateModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.OMS.Infrastructure.EntityConfigurations
{
    public class OrderEntityTypeConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.OrderNo);
            builder.Property(x => x.TotalNum);
            builder.Property(x => x.TotalPrice);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.CustomerId);
            builder.Property(x => x.ContactName);
            builder.Property(x => x.ContactMail);
            builder.Property(x => x.ContactPhone);
            builder.Property(x => x.ShippingAddress);

            var navigation = builder.Metadata.FindNavigation(nameof(Order.OwnOrderItems));
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);

            builder.HasMany(x => x.OwnOrderItems).WithOne(s => s.Order).OnDelete(DeleteBehavior.Cascade);
        }
    }
}
