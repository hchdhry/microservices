using AutoMapper;
using mango.services;
using mango.services.ShoppingCart.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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


    }
}
