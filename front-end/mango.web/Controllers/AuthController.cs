using mango.web.models.DT;
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
       
        public async Task<ResponseDTO> Login([FromBody]LoginDTO loginDTO)
        {
            var response =await _AuthService.LoginAsync(loginDTO);
            return response;
        }
    }
}
