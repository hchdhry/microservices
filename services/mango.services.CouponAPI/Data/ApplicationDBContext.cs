using System;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mango.services.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options) : base(options)
        {
        }

        public DbSet<coupon> Coupons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<coupon>().HasData(
                new coupon
                {
                    couponID = 1,
                    CouponCode = "10OFF",
                    DiscountAmount = 10,
                    MinAmount = 20,
                    LastUpdated = DateTime.UtcNow
                },
                new coupon
                {
                    couponID = 2,
                    CouponCode = "20OFF",
                    DiscountAmount = 20,
                    MinAmount = 40,
                    LastUpdated = DateTime.UtcNow  
                }
            );
        }
    }
}