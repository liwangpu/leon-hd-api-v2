using App.MoreJee.Domain.AggregateModels.CategoryAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Icon);
            builder.Property(x => x.LValue);
            builder.Property(x => x.RValue);
            builder.Property(x => x.NodeType);
            builder.Property(x => x.Resource);
            builder.Property(x => x.DisplayIndex);
            builder.Property(x => x.ParentId);
            builder.Property(x => x.Fingerprint);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
            builder.Property(x => x.OrganizationId);
        }
    }
}
