using AutoMapper;
using mango.services.ProductAPI.Data;
using Mango.Services.ProductAPI.Models;
using Mango.Services.ProductAPI.Models.DTOs;

namespace mango.services
{
    public class MappingConfig : Profile
    {
        private readonly ApplicationDBContext _dbContext;
        public MappingConfig()
        {
           
        CreateMap<Product,ProductDTO>();
     
        }
    }
}
