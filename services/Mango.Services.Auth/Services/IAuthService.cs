
using mango.services.Auth.DTO;

namespace mango.services.Auth.Services{
public interface IAuthService
{
    Task<string> Register(RegisterDTO registerDTO);
    Task<LoginResponseDTO> LogIn(LoginDTO loginDTO);


}
}