using mango.web.Service.IService;

namespace mango.web.Service;
public class TokenProvider : ITokenProvider
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    public TokenProvider(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }
    public void ClearToken()
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Delete("JWTToken");
    }

    public string? GetToken()
    {
        string? token = null;
        bool hasToken = _httpContextAccessor.HttpContext.Request.Cookies.TryGetValue("JWTToken",out token);
        return hasToken is true?token:null;
    }

    public void SetToken(string token)
    {
        _httpContextAccessor.HttpContext.Response.Cookies.Append("JWTToken",token);
    }
}