using System;
using mango.web.models.DT;
using mango.web.models.DTO;
using mango.web.Models.DTO;
using mango.web.Service.IService;
using mango.web.Utilities;

namespace mango.web.Service;

public class AuthService : IAuthService
{
    private readonly IBaseService _baseService;
    public AuthService(IBaseService baseService)
    {
        _baseService = baseService;

    }
    public Task<ResponseDTO> AssignRoleAsync(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<ResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.AuthAPIBase + "/api/Login",
            Data = loginDTO
        });
    }

    public Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        throw new NotImplementedException();
    }
}
