using System;
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
    public async Task<ResponseDTO> AssignRoleAsync(RegisterDTO registerDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.AuthAPIBase + "/api/Auth/AssignRole",
            Data = registerDTO
        },WithBearer: true);
    }

    public async Task<ResponseDTO> LoginAsync(LoginDTO loginDTO)
    {
        var response =  await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.AuthAPIBase + "/api/Auth/login",
            Data = loginDTO
        },WithBearer:false);
        return response;
    }

    public async Task<ResponseDTO> RegisterAsync(RegisterDTO registerDTO)
    {
        return await _baseService.SendAsync(new RequestDTO
        {
            APItype = SD.APIType.POST,
            Url = SD.AuthAPIBase + "/api/Auth/register",
            Data = registerDTO
        },WithBearer:false);
    }
}