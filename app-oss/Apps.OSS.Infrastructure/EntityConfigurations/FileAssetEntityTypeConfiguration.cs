using Apps.OSS.Domain.AggregateModels.FileAssetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Apps.OSS.Infrastructure.EntityConfigurations
{
    public class FileAssetEntityTypeConfiguration : IEntityTypeConfiguration<FileAsset>
    {
        public void Configure(EntityTypeBuilder<FileAsset> builder)
        {
            builder.Ignore(o => o.DomainEvents);
            builder.HasKey(b => b.Id);
            builder.Property(o => o.Name);
            builder.Property(o => o.Description);
            builder.Property(o => o.Size);
            builder.Property(o => o.FileExt);
            builder.Property(o => o.FileState);
            builder.Property(o => o.Creator);
            builder.Property(o => o.Modifier);
            builder.Property(o => o.CreatedTime);
            builder.Property(o => o.ModifiedTime);
        }
    }
}
