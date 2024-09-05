using System;
using mango.web.models.DTO;
using mango.web.Models.DTO;

namespace mango.web.Service.IService;

public interface ICouponService
{
    Task<ResponseDTO> GetCouponByIdAsync<T>(int couponID);
    Task<ResponseDTO> GetAllCouponsAsync<T>();
    Task<ResponseDTO> CreateCoupon<T>(CouponDTO couponDTO);
    Task<ResponseDTO> UpdateCoupon<T>(CouponDTO couponDTO);
    Task<ResponseDTO> DeleteCoupon<T>(int couponID);

}
