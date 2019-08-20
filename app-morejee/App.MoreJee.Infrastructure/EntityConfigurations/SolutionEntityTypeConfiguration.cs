using App.MoreJee.Domain.AggregateModels.DesignAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class SolutionEntityTypeConfiguration : IEntityTypeConfiguration<Solution>
    {
        public void Configure(EntityTypeBuilder<Solution> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Icon);
            builder.Property(x => x.Description);
            //builder.Property(x => x.Properties);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
        }
    }
}
