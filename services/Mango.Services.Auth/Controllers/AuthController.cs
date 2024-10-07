using mango.services.Auth.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Auth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly ApplicationDBContext _dbContext;
        public AuthController(ApplicationDBContext dBContext)
        {
            _dbContext = dBContext;
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register()
        {
            return Ok();

        }
        [HttpPost("Login")]
        public async Task<IActionResult> Login()
        {
            return Ok();

        }
    }
}
