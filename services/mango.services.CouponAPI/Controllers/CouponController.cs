using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mango.services.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;
using mango.services.models.DTOs;


namespace mango.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDBContext _DB;
        private readonly ResponseDTO _responseDTO;
        public CouponController(ApplicationDBContext dBContext, ResponseDTO responseDTO)
        {
            _DB = dBContext;
            _responseDTO = responseDTO;

        }
        [HttpGet]

        public async Task<ResponseDTO> Get()
        {
            try
            {
              _responseDTO.Result = await _DB.Coupons.ToListAsync(); 
              
            }
            catch(Exception e)
            {
               _responseDTO.Message = e.Message;
               _responseDTO.Result = Enumerable.Empty<coupon>;

            }
            return _responseDTO;
        }
        
        [HttpGet]
        [Route("id:int")]
        public async Task<ResponseDTO> Get(int id)
        {
            try
            {
                _responseDTO.Result = await _DB.Coupons.FirstAsync(u=>u.couponID == id);
                _responseDTO.isSuccess = true;
                
            }
            catch (Exception e)
            {
                _responseDTO.Message = e.Message;

            }
            return _responseDTO;
        }
        
        [HttpPost]
        [Route("create")]
        public async Task<ResponseDTO> Post([FromBody] coupon coupon)
        {
            try
            {
                await _DB.Coupons.AddAsync(coupon);
                await _DB.SaveChangesAsync();
                _responseDTO.isSuccess = true;
                
            }
            catch (Exception e)
            {   
              
                _responseDTO.Message = e.Message;
            }
            return _responseDTO;
        }





    }
}
