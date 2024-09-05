using mango.web.models.DTO;
using mango.web.Models;
using mango.web.Models.DTO;
namespace mango.web.Service.IService;

public interface IBaseService 
{
    Task<ResponseDTO> SendAsync(RequestDTO requestDTO);
} 