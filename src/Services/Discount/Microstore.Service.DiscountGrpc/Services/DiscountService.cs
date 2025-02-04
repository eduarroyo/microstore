using Grpc.Core;
using Mapster;
using Microstore.Service.DiscountGrpc.Models;

namespace Microstore.Service.DiscountGrpc.Services;

public class DiscountService
    (DiscountContext dbContext, ILogger<DiscountService> logger)
    : DiscountProtoService.DiscountProtoServiceBase
{
    public override async Task<CouponModel> GetDiscount(GetDiscountRequest request, ServerCallContext context)
    {
        Coupon? coupon = await dbContext.Coupons
            .FirstOrDefaultAsync(c => c.ProductName == request.ProductName);
        if(coupon is null)
        {
            coupon = new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Desc" };
        }

        logger.LogInformation("Discount is retrieved for ProductName: {productName}, Amount: {amount}",
                                coupon.ProductName, coupon.Amount);

        CouponModel couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override async Task<CouponModel> CreateDiscount(CreateDiscountRequest request, ServerCallContext context)
    {
        Coupon coupon = request.Coupon.Adapt<Coupon>();
        if(coupon is null)
        {
            throw new RpcException(new Status(StatusCode.InvalidArgument, "Invalid coupon"));
        }

        await dbContext.Coupons.AddAsync(coupon);
        await dbContext.SaveChangesAsync();

        logger.LogInformation("Discount is successfully created. ProductName: {productName}", coupon.ProductName);
        CouponModel couponModel = coupon.Adapt<CouponModel>();
        return couponModel;
    }

    public override Task<CouponModel> UpdateDiscount(UpdateDiscountRequest request, ServerCallContext context)
    {
        return base.UpdateDiscount(request, context);
    }

    public override Task<DeleteDiscountResponse> DeleteDiscount(DeleteDiscountRequest request, ServerCallContext context)
    {
        return base.DeleteDiscount(request, context);
    }

}
