using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using mango.services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.ProductAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        private readonly IMapper _mapper;

        public ProductController(ApplicationDBContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<ResponseDTO>> Get()
        {
            var response = new ResponseDTO();
            try
            {
                var productList = await _dbContext.Products.ToListAsync();
                response.Result = _mapper.Map<IEnumerable<ProductDTO>>(productList);
                response.Message = "Retrieved products successfully.";
                response.isSuccess = true;
            }
            catch (Exception e)
            {
                response.Message = $"An error occurred: {e.Message}";
                response.Result = Enumerable.Empty<ProductDTO>();
                response.isSuccess = false;
                return BadRequest(response);
            }

            return Ok(response);
        }

        [HttpPost]
        public Task<ActionResult<ResponseDTO>> Post()
        {
            throw new NotImplementedException();
        }

        [HttpPut]
        public Task<ActionResult<ResponseDTO>> Update()
        {
            throw new NotImplementedException();
        }
    }
}
