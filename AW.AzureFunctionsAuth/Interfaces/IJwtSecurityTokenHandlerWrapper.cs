using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace AW.AzureFunctionsAuth.Interfaces
{
    public interface IJwtSecurityTokenHandlerWrapper
    {
        ClaimsPrincipal ValidateToken(string? token, TokenValidationParameters? tokenValidationParameters);
    }
}
