using App.Basic.Domain.AggregateModels.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Basic.Infrastructure.EntityConfigurations
{
    public class CustomRoleEntityTypeConfiguration : IEntityTypeConfiguration<CustomRole>
    {
        public void Configure(EntityTypeBuilder<CustomRole> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.OrganizationId);
            builder.Property(x => x.AccessPointKeys);
        }
    }
}
