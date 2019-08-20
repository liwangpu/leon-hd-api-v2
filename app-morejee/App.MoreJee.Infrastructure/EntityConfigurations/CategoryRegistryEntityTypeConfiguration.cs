using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class CategoryRegistryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryRegistry>
    {
        public void Configure(EntityTypeBuilder<CategoryRegistry> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Icon);
            builder.Property(x => x.Resource);
        }
    }
}
