using Discount.Grpc.Data;
using Discount.Grpc.Models;
using Grpc.Core;
using Microsoft.EntityFrameworkCore;

namespace Discount.Grpc.Service;

public class DiscountService(DiscountContext dbContext, ILogger<DiscountService> service)
    : DiscountProtoService.DiscountProtoServiceBase
{
    private readonly DiscountContext _dbContext = dbContext;
    private readonly ILogger<DiscountService> _logger = service;

    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(x => x.ProductName == request.ProductName);
        if (coupon is null)
            coupon = new Coupon() { ProductName = "No Discount", Amount = 0, Description = "No Description" };

        _logger.LogInformation(
            "Discount is retrieved for product Name: {productName}, Amount: {Amount}, Description: {Descript} ",
            coupon.ProductName, coupon.Amount, coupon.Description);

        return new CouponModel()
        {
            ProductName = coupon.ProductName,
            Description = coupon.Description,
            Amount = coupon.Amount,
            Id = coupon.Id
        };
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        var coupon = new Coupon()
        {
            ProductName = request.Coupon.ProductName,
            Description = request.Coupon.Description,
            Amount = request.Coupon.Amount
        };

        _dbContext.Coupons.Add(coupon);

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation(
            "Discount is successfully created. ProductName: {productName}, Description: {Description}, Amount: {Amount}",
            request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);

        return request.Coupon;
    }

    public override async Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        var couponForUpdate = await _dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.Id == request.Coupon.Id);
        if (couponForUpdate is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon id inside request object"));

        couponForUpdate.Amount = request.Coupon.Amount;
        couponForUpdate.Description = request.Coupon.Description;
        couponForUpdate.ProductName = request.Coupon.ProductName;

        _dbContext.Coupons.Update(couponForUpdate);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation(
            "Discount is successfully updated. ProductName: {productName}, Description: {Description}, Amount: {Amount}",
            request.Coupon.ProductName, request.Coupon.Description, request.Coupon.Amount);

        return request.Coupon;
    }

    public override async Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request,
        ServerCallContext context)
    {
        var coupon = await _dbContext.Coupons.FirstOrDefaultAsync(coupon => coupon.ProductName == request.ProductName);
        if (coupon is null)
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon id inside request object"));
        _dbContext.Coupons.Remove(coupon);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation("Discount is successfully deleted, ProductName : {productId}", request.ProductName);

        return new DeleteDiscountResponse() { Success = true };
    }
}
