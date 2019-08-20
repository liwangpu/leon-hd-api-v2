using App.Basic.Domain.AggregateModels.UserAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.Basic.Infrastructure.EntityConfigurations
{
    class OrganizationTreeEntityTypeConfiguration : IEntityTypeConfiguration<OrganizationTree>
    {
        public void Configure(EntityTypeBuilder<OrganizationTree> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(x => x.Name);
            builder.Property(x => x.NodeType);
            builder.Property(x => x.LValue);
            builder.Property(x => x.RValue);
            builder.Property(x => x.ObjId);
            builder.Property(x => x.ParentId);
            builder.Property(x => x.Group);
            builder.Ignore(b => b.ParentObjId);
        }
    }
}
