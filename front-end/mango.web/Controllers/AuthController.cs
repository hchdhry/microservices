
using mango.web.models.DTO;
using mango.web.Service.IService;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;


namespace mango.web.Controllers
{
  
    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        public AuthController(IAuthService AuthService)
        {
            _AuthService = AuthService;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login( LoginDTO loginDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _AuthService.LoginAsync(loginDTO);
            if (response == null || !response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}
