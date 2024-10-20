using mango.web.models.DTO;
using mango.web.Service.IService;
using mango.web.Utilities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;


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
        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem
                {
                    Text = "ADMIN",Value = "ADMIN"
                },
                   new SelectListItem
                {
                    Text = "USER",Value = "USER"
                }
            };
            ViewBag.roleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginDTO loginDTO)
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
        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var response = await _AuthService.RegisterAsync(registerDTO);
            if (response == null || !response.isSuccess)
            {
                return BadRequest(response);
            }

            return Ok(response);
        }

    }
}