using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mango.services.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace mango.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDBContext _DB;
        public CouponController(ApplicationDBContext dBContext)
        {
            _DB = dBContext;

        }
        [HttpGet]

        public async Task<IEnumerable<coupon>> Get()
        {
            try
            {
              IEnumerable<coupon> coupons = await _DB.Coupons.ToListAsync(); 
              return coupons; 
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
                return Enumerable.Empty<coupon>();

            }
        }
        
        [HttpGet]
        [Route("id:int")]
        public async Task<coupon> Get(int id)
        {
            try
            {
                coupon coupon = await _DB.Coupons.FirstAsync(u=>u.couponID == id);
                return coupon;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;

            }
        }
        [HttpPost]
        public async Task<coupon> Post([FromBody] coupon coupon)
        {
            try
            {
                await _DB.Coupons.AddAsync(coupon);
                await _DB.SaveChangesAsync();
                return coupon;
            }
            catch (Exception e)
            {
                // Log the exception (optional)
                return null;
            }
        }





    }
}
