namespace mango.services.Extensions;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

public static class WebApplicationBuilderExtensions
{
    public static void AddAuthentication(this WebApplicationBuilder builder)
    {
        var Secret = builder.Configuration.GetValue<string>("APIsettings:Secret");
        var Issuer = builder.Configuration.GetValue<string>("APIsettings:Issuer");
        var Audience = builder.Configuration.GetValue<string>("APIsettings:Audience");
        var key = Encoding.ASCII.GetBytes(Secret);

        builder.Services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.RequireHttpsMetadata = false;
            options.SaveToken = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidIssuer = Issuer,
                ValidateAudience = true,
                ValidAudience = Audience,
                ValidateLifetime = true,
                RequireExpirationTime = true  // Set to true once working for added security
            };
        });
    }
}