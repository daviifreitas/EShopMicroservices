using Discount.Grpc.Models;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Data;

public class DiscountContext(DbContextOptions<DiscountContext> options) : DbContext(options)
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        List<Coupon> couponsForCreate = new()
        {
            new Coupon()
                { Id = 1, ProductName = "Iphone X", Description = "Apple Iphone X", Amount = 12 },
            new Coupon()
                { Id = 2, ProductName = "Samsung 10", Description = "Samsung 10 smartphone", Amount = 30 }
        };
        modelBuilder.Entity<Coupon>().HasData(couponsForCreate);
    }
}
