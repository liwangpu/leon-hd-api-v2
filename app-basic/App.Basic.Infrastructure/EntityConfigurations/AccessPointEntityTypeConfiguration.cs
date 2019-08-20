using App.Basic.Domain.AggregateModels.PermissionAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Basic.Infrastructure.EntityConfigurations
{
    public class AccessPointEntityTypeConfiguration : IEntityTypeConfiguration<AccessPoint>
    {
        public void Configure(EntityTypeBuilder<AccessPoint> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.PointKey);
            builder.Property(x => x.IsInner);
            builder.Property(x => x.ApplyOranizationTypeIds);
        }
    }
}
