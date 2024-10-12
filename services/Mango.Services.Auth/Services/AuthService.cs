using System;
using mango.services.Auth.Data;
using mango.services.Auth.DTO;
using mango.services.Auth.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Mango.Services.Auth.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDBContext applicationDBContext;
    private readonly UserManager<ApplicationUser> userManager;
    private readonly IJWTService _jWTService;
    private readonly RoleManager<IdentityRole> roleManager;

    public AuthService(ApplicationDBContext _applicationDBContext, UserManager<ApplicationUser> _userManager, RoleManager<IdentityRole> _roleManager,IJWTService jWTService)
    {
        applicationDBContext = _applicationDBContext;
        userManager = _userManager;
        roleManager = _roleManager;
        _jWTService = jWTService;
    }

    public async Task<LoginResponseDTO> LogIn(LoginDTO loginDTO)
    {
      
      
            var user = await applicationDBContext.ApplicationUsers.FirstOrDefaultAsync(u=>u.UserName == loginDTO.UserName);

        UserDTO UserDto = new UserDTO
        {
            ID = user.Id,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        bool IsValid = await userManager.CheckPasswordAsync(user,loginDTO.Password);
            if(user == null || IsValid != true )
            {
                return new LoginResponseDTO
                {
                    userDTO = null,
                    Token = ""

                };
            }
            else return new LoginResponseDTO
            {
                userDTO = UserDto,
                Token = await _jWTService.GenerateToken(user)

            };


    }

    public async Task<string> Register(RegisterDTO registerDTO)
    {

        ApplicationUser newUser = new ApplicationUser
        {
            Email = registerDTO.Email,
            UserName = registerDTO.Email, 
            PhoneNumber = registerDTO.PhoneNumber,
            NormalizedEmail = registerDTO.Email.ToUpper(),
           
        };

        try
        {
     
            var result = await userManager.CreateAsync(newUser, registerDTO.Password);

            if (result.Succeeded)
            {
         
                var user = await userManager.FindByEmailAsync(registerDTO.Email.ToUpper());
                if (user != null)
                {
                    UserDTO userDTO = new()
                    {
                        Email = user.Email,
                        Name = user.Name,
                        PhoneNumber = user.PhoneNumber
                    };
                }

                return $"User with email: {registerDTO.Email} created successfully!";
            }
            else
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return $"Failed to create user: {errors}";
            }
        }
        catch (Exception e)
        {
           
            return $"An error occurred: {e.Message}";
        }
    }
}
