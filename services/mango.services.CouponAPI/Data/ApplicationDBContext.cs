using System;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;
namespace mango.services.Data;

public class ApplicationDBContext:DbContext
{
    public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options):base(options)
    {

    }

    public DbSet<coupon> coupons {get;set;}

}
