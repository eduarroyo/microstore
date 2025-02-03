using Microsoft.EntityFrameworkCore;
using Microstore.Service.DiscountGrpc.Models;

namespace Microstore.Service.DiscountGrpc.Data;

public class DiscountContext: DbContext
{
    public DbSet<Coupon> Coupons { get; set; } = default!;

    public DiscountContext(DbContextOptions<DiscountContext> options) : base(options)
    {
    }
}
