using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class MapEntityTypeConfiguration : IEntityTypeConfiguration<Map>
    {
        public void Configure(EntityTypeBuilder<Map> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Icon);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
        }
    }
}
