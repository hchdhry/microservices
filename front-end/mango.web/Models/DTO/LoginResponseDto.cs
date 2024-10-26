using System;
using mango.web.Models.DTO;

namespace mango.web.models.DTO;

public class LoginResponseDTO
{
    public UserDTO userDTO { get; set; }
    public string Token { get; set; }
}
