using MyRecipeBook.Domain.Security.Tokens;

namespace MyRecipeBook.API.Token;

public class HttpTokenValue :  ITokenProvider
{
    private readonly IHttpContextAccessor _context;

    public HttpTokenValue(IHttpContextAccessor context)
    {
        _context = context;
    }
    public string Value()
    {
        var authorization = _context.HttpContext!.Request.Headers.Authorization.ToString();
        
        return authorization["Bearer ".Length..].Trim();
    }
}