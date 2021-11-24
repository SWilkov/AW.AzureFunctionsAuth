using AW.AzureFunctionsAuth.Enums;
using AW.AzureFunctionsAuth.Interfaces;
using AW.AzureFunctionsAuth.Models;
using AW.AzureFunctionsAuth.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace AW.AzureFunctionsAuth.Tests
{
  public class TestPrincipal : ClaimsPrincipal
  {
    public TestPrincipal(params Claim[] claims) : base(new TestIdentity(claims))
    {
    }
  }

  public class TestIdentity : ClaimsIdentity
  {
    public TestIdentity(params Claim[] claims) : base(claims)
    {
    }
  }

  public class ClaimsApiPermissionServiceTests
  {
    private IApiPermissionService service;
    public ClaimsApiPermissionServiceTests()
    {
      service = new ClaimsApiPermissionService();
    }

    [Fact]
    public void api_authorization_null_throw_exception()
    {
      Assert.Throws<ArgumentNullException>(() => service.ValidateUserClaims(null, null));
    }

    [Fact]
    public void api_permissions_null_throw_exception()
    {
      var claimsPrinicpal = new TestPrincipal(null);
      var apiAuth = new ApiAuthorizationResult(claimsPrinicpal, "failed");
      Assert.Throws<ArgumentException>(() => service.ValidateUserClaims(apiAuth, null));
    }

    [Fact]
    public void user_claims_null_throw_exception()
    {
      var claimsPrinicpal = new TestPrincipal(null);
      var apiAuth = new ApiAuthorizationResult(claimsPrinicpal, "failed");
      var permissionsToCheck = new string[] { "reader:save,reader:read" };

      Assert.Throws<ArgumentException>(() => service.ValidateUserClaims(apiAuth, permissionsToCheck));
    }

    [Fact]
    public void user_claim_listed_and_one_not_listed_return_unauthorized()
    {
      var claimsPrinicpal = new TestPrincipal(new Claim("permission", "reader:save"));
      var apiAuth = new ApiAuthorizationResult(claimsPrinicpal, "failed");
      var permissionsToCheck = new string[] { "reader:save,reader:read" };

      var result = service.ValidateUserClaims(apiAuth, permissionsToCheck);
      Assert.Equal(AuthResult.UnAuthorized, result);
    }

    [Fact]
    public void user_claims_listed_return_authorized()
    {
      var claimsPrinicpal = new TestPrincipal(new Claim("permission", "reader:save"), new Claim("permission", "reader:read"));
      var apiAuth = new ApiAuthorizationResult(claimsPrinicpal, "success");
      var permissionsToCheck = new string[] { "reader:save","reader:read" };

      var result = service.ValidateUserClaims(apiAuth, permissionsToCheck);
      Assert.Equal(AuthResult.Authorized, result);
    }
  }
}
