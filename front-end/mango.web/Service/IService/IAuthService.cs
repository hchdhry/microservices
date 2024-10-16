using System;
using mango.web.models.DTO;

namespace mango.web.Service.IService;

public interface IAuthService
{
    Task<ResponseDTO> LoginAsync(LoginDTO loginDTO);
    Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO);
    Task<ResponseDTO> AssignRoleAsync(RegisterDTO registerDTO);

}
