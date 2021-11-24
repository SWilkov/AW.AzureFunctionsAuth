using AW.AzureFunctionsAuth.Enums;
using AW.AzureFunctionsAuth.Models;
using AW.AzureFunctionsAuth.Interfaces;

namespace AW.AzureFunctionsAuth.Services
{
  public class ClaimsApiPermissionService : IApiPermissionService
  {
    public AuthResult ValidateUserClaims(ApiAuthorizationResult apiAuthorization, 
      string[] apiPermissions)
    {
      if (apiAuthorization == null || apiAuthorization.ClaimsPrincipal == null
          || apiAuthorization.ClaimsPrincipal.Claims == null || !apiAuthorization.ClaimsPrincipal.Claims.Any())
      {
        throw new ArgumentNullException(nameof(apiAuthorization));
      }

      if (apiPermissions == null || !apiPermissions.Any())
      {
        throw new ArgumentException("no api permissions present");
      }

      //All api permissions need to be found in the User Claims to authorize
      if (apiPermissions.All(x => apiAuthorization.ClaimsPrincipal.Claims.Any(c => c.Value.ToLower() == x.ToLower())))
        return AuthResult.Authorized;

      return AuthResult.UnAuthorized;
    }
  }
}
