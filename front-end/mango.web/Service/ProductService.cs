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
        }, WithBearer: true);
       
    }

    public async Task<ResponseDTO> DeleteCoupon(int id)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.DELETE,
            Url = $"{SD.ProductAPIURL}/api/Product?id={id}",
        }, WithBearer: true);
    }

    public async Task<ResponseDTO> GetAllProductsAsync()
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.GET,
            Url = SD.ProductAPIURL + "/api/Product",
        }, WithBearer: false);
    }

    public async Task<ResponseDTO> GetProductByIdAsync(int ProductId)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.GET,
            Url = $"{SD.ProductAPIURL}/api/Product/{ProductId}",
        }, WithBearer: true);
    }

    public async Task<ResponseDTO> UpdateProduct(ProductDTO productDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.PUT,
            Url = SD.ProductAPIURL + "/api/Product",
            Data = productDTO
        }, WithBearer: true);
    }
}
