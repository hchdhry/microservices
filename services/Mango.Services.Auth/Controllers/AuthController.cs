using mango.services.Auth.Data;
using mango.services.Auth.DTO;
using mango.services.Auth.Services;
using Mango.Services.Auth.Services;
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

            var response = await _authService.Register(registerDTO);
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
    }
}
