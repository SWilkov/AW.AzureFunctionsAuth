using AW.AzureFunctionsAuth.Enums;
using AW.AzureFunctionsAuth.Models;

namespace AW.AzureFunctionsAuth.Interfaces
{
  public interface IApiPermissionService
  {
    AuthResult ValidateUserClaims(ApiAuthorizationResult apiAuthorization, 
      string[] permissions);

    AuthResult ValidateApiPermissionsUserClaim(ApiAuthorizationResult apiAuthorization, string apiPermission);
  }
}
