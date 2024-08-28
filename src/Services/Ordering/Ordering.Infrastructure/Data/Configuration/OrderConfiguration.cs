using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Ordering.Domain.Enums;

namespace Ordering.Infrastructure.Data.Configuration;

public class OrderConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.HasKey(x => x.Id);

        builder.Property(c => c.Id).HasConversion(c => c.Value, bdId => OrderId.Of(bdId));

        builder.HasOne<Customer>()
            .WithMany()
            .HasForeignKey(c => c.CustomerId)
            .IsRequired();

        builder.HasMany(c => c.OrderItems)
            .WithOne()
            .HasForeignKey(c => c.OrderId);

        builder.ComplexProperty(c => c.OrderName, nameBuilder =>
        {
            nameBuilder.Property(n => n.Value).HasColumnName(nameof(Order.OrderName))
                .HasMaxLength(100)
                .IsRequired();
        });

        builder.ComplexProperty(c => c.ShippingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.ZipCode).HasColumnName(nameof(Order.ShippingAddress.ZipCode))
                .HasMaxLength(5)
                .IsRequired();
            addressBuilder.Property(a => a.EmailAddress).HasColumnName(nameof(Order.ShippingAddress.EmailAddress))
                .HasMaxLength(50);
            addressBuilder.Property(a => a.State).HasColumnName(nameof(Order.ShippingAddress.State))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(a => a.Country).HasColumnName(nameof(Order.ShippingAddress.Country))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(c => c.AddressLine).HasColumnName(nameof(Order.ShippingAddress.AddressLine))
                .HasMaxLength(50);
            addressBuilder.Property(c => c.FirstName).HasColumnName(nameof(Order.ShippingAddress.FirstName))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(c => c.LastName).HasColumnName(nameof(Order.ShippingAddress.LastName))
                .HasMaxLength(50)
                .IsRequired();
        });
        
        builder.ComplexProperty(c => c.BillingAddress, addressBuilder =>
        {
            addressBuilder.Property(a => a.ZipCode).HasColumnName(nameof(Order.ShippingAddress.ZipCode))
                .HasMaxLength(5)
                .IsRequired();
            addressBuilder.Property(a => a.EmailAddress).HasColumnName(nameof(Order.ShippingAddress.EmailAddress))
                .HasMaxLength(50);
            addressBuilder.Property(a => a.State).HasColumnName(nameof(Order.ShippingAddress.State))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(a => a.Country).HasColumnName(nameof(Order.ShippingAddress.Country))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(c => c.AddressLine).HasColumnName(nameof(Order.ShippingAddress.AddressLine))
                .HasMaxLength(50);
            addressBuilder.Property(c => c.FirstName).HasColumnName(nameof(Order.ShippingAddress.FirstName))
                .HasMaxLength(50)
                .IsRequired();
            addressBuilder.Property(c => c.LastName).HasColumnName(nameof(Order.ShippingAddress.LastName))
                .HasMaxLength(50)
                .IsRequired();
        });

        builder.ComplexProperty(c => c.Payment, paymentBuilder =>
        {
            paymentBuilder.Property(p => p.CardNumber).HasColumnName(nameof(Order.Payment.CardNumber))
                .HasMaxLength(16)
                .IsRequired();

            paymentBuilder.Property(c => c.CardHolderName).HasColumnName(nameof(Order.Payment.CardHolderName))
                .HasMaxLength(50);

            paymentBuilder.Property(c => c.Expiration).HasColumnName(nameof(Order.Payment.Expiration))
                .HasMaxLength(10);
            paymentBuilder.Property(c => c.CVV).HasColumnName(nameof(Order.Payment.CVV))
                .HasMaxLength(3);
            paymentBuilder.Property(c => c.PaymentMethod);
        });

        builder.Property(o => o.OrderStatus).HasDefaultValue(OrderStatus.Draft)
            .HasConversion(s => s.ToString(), dbStatus => (OrderStatus)Enum.Parse(typeof(OrderStatus), dbStatus));
    }
}