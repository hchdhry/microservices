using AutoMapper;
using Mango.Services.CouponAPI.Models;
using mango.services.models.DTOs;

namespace mango.services
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
           
            CreateMap<coupon, CouponDTO>()
                .ForMember(dest => dest.MinAmount, opt => opt.MapFrom(src => src.MinAmount))
                .ForMember(dest => dest.CouponCode, opt => opt.MapFrom(src => src.CouponCode))
                .ForMember(dest => dest.DiscountAmount, opt => opt.MapFrom(src => src.DiscountAmount))
                .ForMember(dest => dest.couponID, opt => opt.MapFrom(src => src.couponID));

           
            CreateMap<CouponDTO, coupon>()
                .ForMember(dest => dest.LastUpdated, opt => opt.Ignore()); 
        }
    }
}
