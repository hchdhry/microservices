using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using mango.services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;

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
        [HttpGet("{id}")]
        public async Task<ActionResult> GetById([FromRoute] int id)
        {
            ResponseDTO response = new();
            try
            {
                var product = await _dbContext.Products.FirstOrDefaultAsync(p => p.ProductId == id);

                if (product != null)
                {
                    response.Result = _mapper.Map<ProductDTO>(product);
                    response.Message = "Retrieved product successfully.";
                    response.isSuccess = true;
                    return Ok(response);
                }
                else
                {
                    response.Message = $"No product found with ID {id}.";
                    response.isSuccess = false;
                    return NotFound(response);
                }
            }
            catch (Exception e)
            {
                response.Message = $"An error occurred: {e.Message}";
                response.Result = null;
                response.isSuccess = false;
                return BadRequest(response);
            }
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
       
        public async Task<ActionResult<ResponseDTO>> Update(int id, ProductDTO productDTO)
        {
            if (id <= 0)
            {
                return BadRequest(new ResponseDTO
                {
                    Result = null,
                    Message = "Invalid product ID",
                    isSuccess = false
                });
            }

            try
            {
                var productToUpdate = await _dbContext.Products
                    .FirstOrDefaultAsync(u => u.ProductId == id);

                if (productToUpdate == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Result = null,
                        Message = "Product not found",
                        isSuccess = false
                    });
                }

              
                _mapper.Map(productDTO, productToUpdate);

                _dbContext.Products.Update(productToUpdate);
                await _dbContext.SaveChangesAsync();

                return Ok(new ResponseDTO
                {
                    Result = productToUpdate,
                    Message = "Product updated successfully.",
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
        [HttpDelete]
       
        public async Task<ActionResult<ResponseDTO>>Delete(int id)
        {
            try
            {
               Product ProductToDelete = await _dbContext.Products.FirstOrDefaultAsync(u=>u.ProductId == id);
                if (ProductToDelete == null)
                {
                    return NotFound(new ResponseDTO
                    {
                        Result = null,
                        Message = $"Product with ID {id} not found.",
                        isSuccess = false
                    });
                }
                _dbContext.Products.Remove(ProductToDelete);
               await _dbContext.SaveChangesAsync();
                return Ok(new ResponseDTO
                {
                    Result = _mapper.Map<ProductDTO>(ProductToDelete),
                    Message = "Product deleted successfully.",
                    isSuccess = true
                });
            }
            catch(Exception e)
            {
                return BadRequest(new ResponseDTO
                {
                    Result = "",
                    Message = $"An error occurred: {e.Message}",
                    isSuccess = false
                });

            }

        }
    }
}
