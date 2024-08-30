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

    }
}
