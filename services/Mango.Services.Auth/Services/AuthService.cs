using System;
using mango.services.Auth.Data;
using mango.services.Auth.DTO;
using mango.services.Auth.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;

namespace Mango.Services.Auth.Services;

public class AuthService : IAuthService
{
    private readonly ApplicationDBContext applicationDBContext;
    private readonly UserManager<ApplicationUser> userManager;

    private readonly RoleManager<IdentityRole> roleManager;

    public AuthService(ApplicationDBContext _applicationDBContext,UserManager<ApplicationUser> _userManager,RoleManager<IdentityRole> _roleManager)
    {
        applicationDBContext = _applicationDBContext;
        userManager = _userManager;
        roleManager = _roleManager;
    }
    public Task<LoginResponseDTO> LogIn(LoginDTO loginDTO)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDTO> Register(RegisterDTO registerDTO)
    {
        ApplicationUser NewUser = new ()
        {
            Email = registerDTO.Email,
            PasswordHash = registerDTO.Password,
            PhoneNumber = registerDTO.PhoneNumber,
            NormalizedEmail = registerDTO.Email.ToUpper()
            
        };
        try
        {
            var result = await userManager.CreateAsync(NewUser,registerDTO.Password);
            if (result.Succeeded) 
            {
                var user = applicationDBContext.ApplicationUsers.First(u=>u.NormalizedEmail == registerDTO.Email.ToUpper());
                UserDTO userDTO = new()
                {
                    Email = user.Email,
                    ID = user.Id,
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber


                    
                };
                return userDTO;

            }
            return new UserDTO();



        }
        catch(Exception e)
        {
            return new UserDTO();

        }
    }
}
