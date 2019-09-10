using App.Basic.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Basic.Infrastructure.EntityConfigurations
{
    class OrganizationEntityTypeConfiguration : IEntityTypeConfiguration<Organization>
    {
        public void Configure(EntityTypeBuilder<Organization> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Ignore(x => x.DisplayIndex);
            builder.Ignore(x => x.NodeType);
            builder.Ignore(x => x.Fingerprint);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Mail);
            builder.Property(x => x.Phone);
            builder.Property(x => x.Active);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.ParentId);
            builder.Property(x => x.OwnerId);
            builder.Property(x => x.OrganizationTypeId);
            builder.Property(x => x.LValue);
            builder.Property(x => x.RValue);
            builder.Property(x => x.ParentId);
            builder.Property(x => x.Fingerprint);
            var navigation = builder.Metadata.FindNavigation(nameof(Organization.OwnAccounts));
            // DDD Patterns comment:
            //Set as field (New since EF 1.1) to access the OrderItem collection property through its field
            navigation.SetPropertyAccessMode(PropertyAccessMode.Field);
        }
    }
}
