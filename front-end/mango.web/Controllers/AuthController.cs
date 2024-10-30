using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using mango.web.models.DTO;
using mango.web.Service.IService;
using mango.web.Utilities;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;





namespace mango.web.Controllers
{

    public class AuthController : Controller
    {
        private readonly IAuthService _AuthService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService AuthService, ITokenProvider tokenProvider)
        {
            _AuthService = AuthService;
            _tokenProvider = tokenProvider;
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpGet]
        [Authorize(Roles ="ADMIN")]
        public IActionResult AssignRole()
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
            try
            {
                var response = await _AuthService.LoginAsync(loginDTO);

                var responseContent = JsonConvert.SerializeObject(response);



                var loginResponseDTO = JsonConvert.DeserializeObject<LoginResponseDTO>(responseContent);

                if (loginResponseDTO == null || string.IsNullOrEmpty(loginResponseDTO.Token))
                {
                    Console.WriteLine("LoginResponseDTO is null or token is empty");
                    ModelState.AddModelError("CustomError", "LoginResponseDTO is null or token is empty.");
                    return View(loginDTO);
                }


                await SignInUser(loginResponseDTO);
                _tokenProvider.SetToken(loginResponseDTO.Token);
                return RedirectToAction("index", "Home");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("CustomError", "An unexpected error occurred");
                return View(loginDTO);
            }
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
        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("index", "Home");
        }
        private async Task SignInUser(LoginResponseDTO model)
        {
            if (string.IsNullOrEmpty(model?.Token))
            {
                throw new NullReferenceException("Token is null or empty.");
            }

            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);

            var emailClaim = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email)?.Value;
            if (!string.IsNullOrEmpty(emailClaim))
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Email, emailClaim));
                identity.AddClaim(new Claim(ClaimTypes.Name, emailClaim));
            }

            var subClaim = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub)?.Value;
            if (!string.IsNullOrEmpty(subClaim))
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Sub, subClaim));
            }

            var nameClaim = jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name)?.Value;
            if (!string.IsNullOrEmpty(nameClaim))
            {
                identity.AddClaim(new Claim(JwtRegisteredClaimNames.Name, nameClaim));
            }

        
            var roleClaim = jwt.Claims.FirstOrDefault(u => u.Type == "role")?.Value;
            if (!string.IsNullOrEmpty(roleClaim))
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleClaim));
            }

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }


    }
}