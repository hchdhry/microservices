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
        public async Task<ActionResult<ResponseDTO>> Post(ProductDTO productDTO)
        {
            try
            {
             
                Product product = _mapper.Map<Product>(productDTO);

         
                await _dbContext.Products.AddAsync(product);
                await _dbContext.SaveChangesAsync();

                var resultDTO = _mapper.Map<ProductDTO>(product);

                return Ok(new ResponseDTO
                {
                    Result = resultDTO,
                    Message = "Product created successfully.",
                    isSuccess = true
                });
            }
            catch (Exception e)
            {
                return BadRequest(new ResponseDTO
                {
                    Result = null,
                    Message = $"An error occurred: {e.Message}",
                    isSuccess = false
                });
            }
        }

        [HttpPut]
        public Task<ActionResult<ResponseDTO>> Update()
        {
            throw new NotImplementedException();
        }
    }
}
