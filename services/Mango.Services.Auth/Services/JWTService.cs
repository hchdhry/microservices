using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using mango.services.Auth.Models;
using Microsoft.Extensions.Options;

namespace Mango.Services.Auth.Services
{
    public class JWTService : IJWTService
    {
        private readonly JwtOptions _jwtOptions;

        public JWTService(IOptions<JwtOptions> jwtOptions)
        {
            _jwtOptions = jwtOptions.Value;
        }

        public Task<string> GenerateToken(ApplicationUser applicationUser,IEnumerable<string>roles)
        {
            
            var email = applicationUser.Email ?? throw new ArgumentNullException(nameof(applicationUser.Email), "Email cannot be null");
            var id = applicationUser.Id ?? throw new ArgumentNullException(nameof(applicationUser.Id), "User ID cannot be null");
            var name = applicationUser.Name ?? "Anonymous"; 
   

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtOptions.Secret);

            var claims = new List<Claim>
    {
        new Claim(JwtRegisteredClaimNames.Email, email),
        new Claim(JwtRegisteredClaimNames.Sub, id),
        new Claim(JwtRegisteredClaimNames.Name, name),

    };
            claims.AddRange(roles.Select(role=>new Claim(ClaimTypes.Role,role)));


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(4),
                Audience = _jwtOptions.Audience,
                Issuer = _jwtOptions.Issuer,
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return Task.FromResult(tokenHandler.WriteToken(token));
        }

    }
}
