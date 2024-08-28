using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ordering.Infrastructure.Data.Configuration;

public class ProductConfiguration : IEntityTypeConfiguration<Product>
{
    public void Configure(EntityTypeBuilder<Product> builder)
    {
        builder.HasKey(p => p.Id);
        builder.Property(c => c.Id).HasConversion(c => c.Value, bdId => ProductId.Of(bdId));
        
        builder.Property(p => p.Name).HasMaxLength(100).IsRequired();
        
    }
}