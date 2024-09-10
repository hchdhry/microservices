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

        [HttpGet]
        public IActionResult CouponCreate()
        {
            return View();
        }

        
        [HttpPost]
        public async Task<IActionResult> CouponCreate(CouponDTO coupon)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO response = await _couponRepository.CreateCoupon(coupon);
                if (response != null && response.isSuccess)
                {
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    ModelState.AddModelError("", response?.Message ?? "Error creating coupon. Please try again.");
                }
            }
            return View(coupon);
        }
    
        public async Task<IActionResult> CouponDelete(int id)
        {
            ResponseDTO response = await _couponRepository.GetCouponByIdAsync(id);
            if (response != null && response.isSuccess)
            {
               CouponDTO coupons = JsonConvert.DeserializeObject<CouponDTO>(Convert.ToString(response.Result));
                return View(coupons);
            }
            return NotFound();
        }

        [HttpPost]
        
        public async Task<IActionResult> CouponDelete(CouponDTO coupon)
        {
            if (ModelState.IsValid)
            {
                ResponseDTO response = await _couponRepository.DeleteCoupon(coupon.couponID);
                if (response != null && response.isSuccess)
                {
                    TempData["success"] = "Coupon deleted successfully";    
                    return RedirectToAction(nameof(CouponIndex));
                }
                else
                {
                    TempData["error"] = response?.Message ?? "Error deleting coupon. Please try again.";
                    ModelState.AddModelError("", response?.Message ?? "Error deleting coupon. Please try again.");
                    return View(coupon);
     
                }
            }
            foreach (var error in ModelState.Values.SelectMany(v => v.Errors))
            {
                Console.WriteLine(error.ErrorMessage);
            }

            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

    }
}
