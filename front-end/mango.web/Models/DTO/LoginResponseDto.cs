using System;
using mango.web.Models.DTO;
using Newtonsoft.Json;

namespace mango.web.models.DTO;

public class LoginResponseDTO
{
    public UserDTO userDTO { get; set; }
    [JsonProperty("token")]
    public string Token { get; set; }
}
