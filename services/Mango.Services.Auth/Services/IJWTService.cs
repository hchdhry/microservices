using System;

namespace Mango.Services.Auth.Services;

public interface IJWTService
{
    public Task<string> GenerateToken(ApplicationUser applicationUser);
}
