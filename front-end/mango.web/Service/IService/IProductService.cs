using mango.web.models.DTO;

namespace mango.web.Service.IService;
public interface IProductService
{
    Task<ResponseDTO> GetProductByIdAsync(int ProductId);
    Task<ResponseDTO> GetAllProductsAsync();
    Task<ResponseDTO> CreateProduct(ProductDTO productDTODTO);
    Task<ResponseDTO> UpdateProduct(ProductDTO productDTO);
    Task<ResponseDTO> DeleteCoupon(int id);

}