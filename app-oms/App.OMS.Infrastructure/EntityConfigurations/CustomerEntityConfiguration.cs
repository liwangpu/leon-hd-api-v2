using App.OMS.Domain.AggregateModels.CustomerAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.OMS.Infrastructure.EntityConfigurations
{
    public class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(b => b.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Company);
            builder.Property(x => x.Phone);
            builder.Property(x => x.Mail);
            builder.Property(x => x.Mail);
            builder.Property(x => x.Address);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);

            var navigation = builder.Metadata.FindNavigation(nameof(Customer.OwnOrders));
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
