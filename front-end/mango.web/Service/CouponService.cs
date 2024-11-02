using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Service.IService;
using mango.web.Utilities;

namespace mango.web.Service;
public class CouponService : ICouponService
{
    private readonly IBaseService _baseService;
    public CouponService(IBaseService baseService) 
    {
        _baseService = baseService;
    }
    public async Task<ResponseDTO> CreateCoupon(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.CouponAPIURL+"/api/coupon",
            Data = couponDTO
        }, WithBearer: true);
    }

    public async Task<ResponseDTO> DeleteCoupon(int couponID)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.DELETE,
            Url = $"{SD.CouponAPIURL}/api/Coupon/{couponID}"
        }, WithBearer: true);
    }

    public async Task<ResponseDTO> GetAllCouponsAsync()
    {
       return await _baseService.SendAsync(new RequestDTO
       {
           APItype = SD.APIType.GET,
           Url = SD.CouponAPIURL+"/api/Coupon",
       }, WithBearer: true);
    }

    public async Task<ResponseDTO> GetCouponByIdAsync(int couponID)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.GET,
            Url = SD.CouponAPIURL+"/api/Coupon/"+couponID,
        }, WithBearer: true);
    }
    
        
    

    public async Task<ResponseDTO> UpdateCoupon(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.PUT,
            Url = SD.CouponAPIURL+"/api/Coupon",
            Data = couponDTO
        }, WithBearer: true);
    }
}