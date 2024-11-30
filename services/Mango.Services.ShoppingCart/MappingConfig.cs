using AutoMapper;
using mango.services.ShoppingCart.Data;
using Mango.Services.ShoppingCart.Models;
using Mango.Services.ShoppingCart.Models.Dto;
using Mango.Services.ShoppingCart.Models.DTOs;


namespace mango.services
{
    public class MappingConfig : Profile
    {
        private readonly ApplicationDBContext _dbContext;
        public MappingConfig()
        {

            CreateMap<CartDetails, CartDetailsDto>();
            CreateMap<CartDetailsDto, CartDetails>();

            CreateMap<CartHeader, CartHeaderDto>();
            CreateMap<CartHeaderDto, CartHeader>();

        }
    }
}
