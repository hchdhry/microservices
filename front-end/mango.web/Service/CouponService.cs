using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Service;
using mango.web.Service.IService;
using mango.web.Utilities;

public class couponService : ICouponService
{
    private readonly IBaseService _baseService;
    public couponService(IBaseService baseService) 
    {
        _baseService = baseService;
    }
    public async Task<ResponseDTO> CreateCoupon<T>(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.CouponAPIURL+"/api/coupon",
            Data = couponDTO
        });
    }

    public async Task<ResponseDTO> DeleteCoupon<T>(int couponID)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.DELETE,
            Url = SD.CouponAPIURL+"/api/coupon/"+couponID,
        });
    }

    public async Task<ResponseDTO> GetAllCouponsAsync<T>()
    {
       return await _baseService.SendAsync(new RequestDTO
       {
           APItype = SD.APIType.GET,
           Url = SD.CouponAPIURL+"/api/coupon",
       });
    }

    public async Task<ResponseDTO> GetCouponByIdAsync<T>(int couponID)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.GET,
            Url = SD.CouponAPIURL+"/api/coupon/"+couponID,
        });
    }
    
        
    

    public async Task<ResponseDTO> UpdateCoupon<T>(CouponDTO couponDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.PUT,
            Url = SD.CouponAPIURL+"/api/coupon",
            Data = couponDTO
        });
    }
}