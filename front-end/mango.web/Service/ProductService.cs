using System;
using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Utilities;

namespace mango.web.Service.IService;

public class ProductService : IProductService
{
    private readonly IBaseService _baseService;
    public ProductService(IBaseService baseService)
    {
        _baseService = baseService;

    }
    public Task<ResponseDTO> CreateProduct(ProductDTO productDTODTO)
    {
        return _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.ProductAPIURL + "/api/Product",
            Data = productDTODTO
        }, WithBearer: false);
       
    }

    public Task<ResponseDTO> DeleteCoupon(int ProductDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO> GetAllProductsAsync()
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.GET,
            Url = SD.ProductAPIURL + "/api/Product",
        }, WithBearer: false);
    }

    public Task<ResponseDTO> GetProductByIdAsync(int ProductId)
    {
        throw new NotImplementedException();
    }

    public Task<ResponseDTO> UpdateCoupon(ProductDTO productDTO)
    {
        throw new NotImplementedException();
    }
}
