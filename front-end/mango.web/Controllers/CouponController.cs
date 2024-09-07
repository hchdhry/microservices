using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Service.IService;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace mango.web.Controllers
{
  
    public class CouponController : Controller
    {
        private readonly ICouponService _couponRepository;
        public CouponController(ICouponService couponService)
        {
            _couponRepository = couponService;
            
        }
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDTO> coupons = new();
            ResponseDTO response = await _couponRepository.GetAllCouponsAsync();
            if(response != null && response.isSuccess)
            {
                coupons = JsonConvert.DeserializeObject<List<CouponDTO>>(Convert.ToString(response.Result));
            }
            return View(coupons);
        }

        public async Task<IActionResult> CouponCreate()
        {
            return View();
        }

    }
}
