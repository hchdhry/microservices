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

    public async Task<bool> AssignRole(string Email, string Role)
    {
        var user = await userManager.FindByEmailAsync(Email);
        if(user != null)
        {
            if(!roleManager.RoleExistsAsync(Role).GetAwaiter().GetResult())
            {
                roleManager.CreateAsync(new IdentityRole(Role)).GetAwaiter().GetResult();

            }
            await userManager.AddToRoleAsync(user,Role);
            return true;

        }
        return false;
    }

    public async Task<LoginResponseDTO> LogIn(LoginDTO loginDTO)
    {
        // First, check for the user
        var user = await applicationDBContext.ApplicationUsers
            .FirstOrDefaultAsync(u => u.UserName == loginDTO.UserName);

        // If user not found or password is invalid
        if (user == null || !await userManager.CheckPasswordAsync(user, loginDTO.Password))
        {
            return new LoginResponseDTO
            {
                userDTO = null,
                Token = "" 
            };
        }

      
        var userDto = new UserDTO
        {
            ID = user.Id,
            Email = user.Email,
            Name = user.Name,
            PhoneNumber = user.PhoneNumber
        };

        var role = await userManager.GetRolesAsync(user);
        var token = await _jWTService.GenerateToken(user,role);

      
        return new LoginResponseDTO
        {
            userDTO = userDto,
            Token = token
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
