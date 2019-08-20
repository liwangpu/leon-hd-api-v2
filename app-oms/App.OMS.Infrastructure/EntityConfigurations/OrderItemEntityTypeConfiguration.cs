using App.OMS.Domain.AggregateModels.OrderAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace App.OMS.Infrastructure.EntityConfigurations
{
    public class OrderItemEntityTypeConfiguration : IEntityTypeConfiguration<OrderItem>
    {
        public void Configure(EntityTypeBuilder<OrderItem> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Ignore(b => b.DomainEvents);
            builder.Ignore(b => b.TotalPrice);
            builder.Property(x => x.ProductId);
            builder.Property(x => x.ProductName);
            builder.Property(x => x.ProductDescription);
            builder.Property(x => x.ProductIcon);
            builder.Property(x => x.ProductBrand);
            builder.Property(x => x.ProductUnit);
            builder.Property(x => x.ProductSpecId);
            builder.Property(x => x.ProductSpecName);
            builder.Property(x => x.ProductSpecDescription);
            builder.Property(x => x.ProductSpecIcon);
            builder.Property(x => x.Num);
            builder.Property(x => x.UnitPrice);
            builder.Property(x => x.Remark);
        }
    }
}
