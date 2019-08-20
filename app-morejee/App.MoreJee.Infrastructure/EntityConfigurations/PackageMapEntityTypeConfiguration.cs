using App.MoreJee.Domain.AggregateModels.ClientAssetAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.MoreJee.Infrastructure.EntityConfigurations
{
    public class PackageMapEntityTypeConfiguration : IEntityTypeConfiguration<PackageMap>
    {
        public void Configure(EntityTypeBuilder<PackageMap> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Property(x => x.Package);
            builder.Property(x => x.Dependencies);
            builder.Property(x => x.SourceAssetUrl);
            builder.Property(x => x.UnCookedAssetUrl);
            builder.Property(x => x.Win64CookedAssetUrl);
            builder.Property(x => x.AndroidCookedAssetUrl);
            builder.Property(x => x.IOSCookedAssetUrl);
            builder.Property(x => x.DependencyAssetUrlsOfWin64Cooked);
            builder.Property(x => x.DependencyAssetUrlsOfAndroidCooked);
            builder.Property(x => x.DependencyAssetUrlsOfIOSCooked);
            builder.Property(x => x.ResourceId);
            builder.Property(x => x.ResourceType);
        }
    }
}
