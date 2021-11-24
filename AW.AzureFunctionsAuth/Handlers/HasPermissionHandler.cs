using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AW.AzureFunctionsAuth.Handlers
{
  public class HasPermissionHandler : AuthorizationHandler<HasPermissionRequirement>
  {
    protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, HasPermissionRequirement requirement)
    {
      if (!context.User.HasClaim(c => c.Type == "permissions" && c.Issuer == requirement.Issuer))
        return Task.CompletedTask;

      var permissions = context.User.FindAll(c => c.Type == "permissions" && c.Issuer == requirement.Issuer);

      if (permissions.Any(s => s.Value == requirement.Permission))
        context.Succeed(requirement);

      return Task.CompletedTask;
    }
  }

  public class HasPermissionRequirement : IAuthorizationRequirement
  {
    public string Issuer { get; }
    public string Permission { get; }

    public HasPermissionRequirement(string permission, string issuer)
    {
      Permission = permission ?? throw new ArgumentNullException(nameof(permission));
      Issuer = issuer ?? throw new ArgumentNullException(nameof(issuer));
    }
  }
}
