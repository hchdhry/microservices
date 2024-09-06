using System;
using mango.web.models.DTO;
using mango.web.Models.DTO;

namespace mango.web.Service.IService;

public interface ICouponService
{
    Task<ResponseDTO> GetCouponByIdAsync(int couponID);
    Task<ResponseDTO> GetAllCouponsAsync();
    Task<ResponseDTO> CreateCoupon(CouponDTO couponDTO);
    Task<ResponseDTO> UpdateCoupon(CouponDTO couponDTO);
    Task<ResponseDTO> DeleteCoupon(int couponID);

}
