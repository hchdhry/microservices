using System;
using System.ComponentModel.DataAnnotations;

namespace mango.services.models.DTOs;

public class CouponDTO
{
    [Key]
    public int couponID { get; set; }
    [Required]
    public string CouponCode { get; set; }
    [Required]
    public double DiscountAmount { get; set; }

    public int MinAmount { get; set; }
}
