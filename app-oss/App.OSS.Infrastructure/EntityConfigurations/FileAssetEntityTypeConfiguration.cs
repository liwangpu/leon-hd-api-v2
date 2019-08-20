using App.OSS.Domain.AggregateModels.FileAssetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.OSS.Infrastructure.EntityConfigurations
{
    class FileAssetEntityTypeConfiguration : IEntityTypeConfiguration<FileAsset>
    {
        public void Configure(EntityTypeBuilder<FileAsset> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Name);
            builder.Property(x => x.Description);
            builder.Property(x => x.Size);
            builder.Property(x => x.FileExt);
            builder.Property(x => x.FileState);
            builder.Property(x => x.Creator);
            builder.Property(x => x.Modifier);
            builder.Property(x => x.CreatedTime);
            builder.Property(x => x.ModifiedTime);
        }
    }
}
