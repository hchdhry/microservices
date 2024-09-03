using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using mango.services.Data;
using Mango.Services.CouponAPI.Models;
using Microsoft.EntityFrameworkCore;
using mango.services.models.DTOs;
using AutoMapper;

namespace mango.services.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ApplicationDBContext _db;
        private readonly IMapper _mapper;

   
        public CouponController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _db = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> Get()
        {
            var responseDTO = new ResponseDTO();
            try
            {
                var couponList = await _db.Coupons.ToListAsync();
                responseDTO.Result = couponList;
                responseDTO.isSuccess = true;
            }
            catch (Exception e)
            {
                responseDTO.Message = e.Message;
                responseDTO.Result = Enumerable.Empty<coupon>(); 
            }

            return Ok(responseDTO);
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<ActionResult<ResponseDTO>> Get(int id)
        {
            var responseDTO = new ResponseDTO();
            try
            {
                var obj = await _db.Coupons.FirstOrDefaultAsync(u => u.couponID == id);
                if (obj == null)
                {
                    responseDTO.Message = "Coupon not found";
                    return NotFound(responseDTO);
                }
                responseDTO.Result = _mapper.Map<CouponDTO>(obj);
                responseDTO.isSuccess = true;
            }
            catch (Exception e)
            {
                responseDTO.Message = e.Message;
            }

            return Ok(responseDTO);
        }

        [HttpPost]
        [Route("create")]
        public async Task<ActionResult<ResponseDTO>> Post([FromBody] coupon coupon)
        {
            var responseDTO = new ResponseDTO();
            try
            {
                if (coupon == null)
                {
                    responseDTO.Message = "Invalid coupon data";
                    return BadRequest(responseDTO);
                }

                await _db.Coupons.AddAsync(coupon);
                await _db.SaveChangesAsync();
                responseDTO.isSuccess = true;
                responseDTO.Result = coupon;
            }
            catch (Exception e)
            {
                responseDTO.Message = e.Message;
                return StatusCode(StatusCodes.Status500InternalServerError, responseDTO);
            }

            return CreatedAtAction(nameof(Get), new { id = coupon.couponID }, responseDTO);
        }
    }
}
