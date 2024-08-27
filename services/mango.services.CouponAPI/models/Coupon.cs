using System.ComponentModel.DataAnnotations;

namespace Mango.Services.CouponAPI.Models;
public class coupon
{
    [Key]
    public int couponID {get;set;}
    [Required]
    public string CouponCode {get;set;}
    [Required]
    public double DiscountAmount{get;set;}

    public int MinAmount{get;set;}
    public DateTime LastUpdated {get;set;}
} 