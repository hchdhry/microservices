namespace Mango.Services.CouponAPI.Models
public class coupon
{
    public int couponID {get;set;}
    public string CouponCode {get;set;}

    public double DiscountAmount{get;set;}

    public int MinAmount{get;set;}
    public DateTime LastUpdated {get;set;}
} 