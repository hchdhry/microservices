using mango.services.Auth.Data;
using mango.services.Auth.DTO;
using mango.services.Auth.Services;
using Mango.Services.Auth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ApplicationDBContext _dbContext;
        public AuthController(ApplicationDBContext dBContext,IAuthService authService)
        {
            _dbContext = dBContext;
            _authService = authService;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register([FromBody]RegisterDTO registerDTO)
        {

            var Data = await _authService.Register(registerDTO);
            var response = new ResponseDTO
            {
                Result = Data,
                isSuccess = true,
                Message = "user registered successfully"    
            };
            return Ok(response);

        }

        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody]LoginDTO loginDTO)
        {
            var response = await _authService.LogIn(loginDTO);
            if (response.userDTO == null)
            {
                return BadRequest(response);
            }
            return Ok(response);


        }
        [HttpPost]
        [Authorize(Roles = "ADMIN")]
        [Route("AssignRole")]
        public async Task<IActionResult> AssignRole([FromBody] RegisterDTO Model)
        {
            var response = await _authService.AssignRole(Model.Email,Model.RoleName);
            if (response == false)
            {
               
                return BadRequest(response);
            }
            return Ok(response);


        }
    }
}
