using AutoMapper;
using mango.services;
using mango.services.ShoppingCart.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Mango.Services.ShoppingCart.Models;
using Mango.Services.ShoppingCart.Models.DTOs;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ShoppingCart
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShoppingCartAPIController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;
        public ShoppingCartAPIController(IMapper mapper, ApplicationDBContext applicationDBContext)
        {
            _dbContext = applicationDBContext;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<ResponseDTO> Upsert(CartDto cartDto)
        {
            ResponseDTO response = new ResponseDTO();
            try
            {
           
            CartHeader cartHeaderFromDB = await _dbContext.CartHeaders.FirstOrDefaultAsync(u=>u.UserId == cartDto.CartHeader.UserId);
            if (cartHeaderFromDB == null)
            {
                CartHeader cartHeader =   _mapper.Map<CartHeader>(cartDto.CartHeader);
                await _dbContext.CartHeaders.AddAsync(cartHeader);
                await _dbContext.SaveChangesAsync();
                cartDto.CartDetails.First().CartHeaderId = cartHeader.CartHeaderId;
                await _dbContext.CartDetails.AddAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                await _dbContext.SaveChangesAsync();

            } 
            else
            {
                var CartDetailsFromDB = await _dbContext.CartDetails.FirstOrDefaultAsync(u=>
                u.ProductId == cartDto.CartDetails.First().ProductId && u.CartHeaderId == cartHeaderFromDB.CartHeaderId);
                if(CartDetailsFromDB == null)
                {
                        cartDto.CartDetails.First().CartHeaderId = cartHeaderFromDB.CartHeaderId;
                        await _dbContext.CartDetails.AddAsync(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                        await _dbContext.SaveChangesAsync();

                    }
                else
                {
                    cartDto.CartDetails.First().Count+= CartDetailsFromDB.Count;
                    cartDto.CartDetails.First().CartHeaderId = CartDetailsFromDB.CartHeaderId;
                    cartDto.CartDetails.First().CartDetailsId = CartDetailsFromDB.CartDetailsId;
                    _dbContext.CartDetails.Update(_mapper.Map<CartDetails>(cartDto.CartDetails.First()));
                    await _dbContext.SaveChangesAsync();

                }
            }
            response.Result = cartDto;
            }
            catch(Exception e)
            {
                response.IsSuccess = false;
                response.Result = new List<string>() { e.Message };
                response.Message = "An error occurred";
                return response;
            } 
            return response;
        }


 

    }
}
