using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderItemConfiguration : IEntityTypeConfiguration<OrderItem>
{
    public void Configure(EntityTypeBuilder<OrderItem> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Id).HasConversion(c => c.Value, bdId => OrderItemId.Of(bdId));

        builder.HasOne<Product>()
            .WithMany()
            .HasForeignKey(c => c.ProductId);
        
        builder.Property(c => c.Quantity).IsRequired();
        builder.Property(c => c.Price).IsRequired();
    }
}