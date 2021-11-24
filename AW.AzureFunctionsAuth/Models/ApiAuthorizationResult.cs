using System.Security.Claims;

namespace AW.AzureFunctionsAuth.Models
{
  /// <summary>
  /// Encapsulates the results of an API authorization with or without Claims.
  /// </summary>
  public class ApiAuthorizationResult
  {
    public ClaimsPrincipal? ClaimsPrincipal { get; private set; }
    /// <summary>
    /// True if authorization failed.
    /// </summary>
    public bool Success => string.IsNullOrWhiteSpace(FailureReason);

    /// <summary>
    /// String describing the reason for the authorization failure.
    /// </summary>
    public string FailureReason { get; }
    /// <summary>
    /// Constructs a success authorization.
    /// </summary>
    public ApiAuthorizationResult(ClaimsPrincipal principal, string failureReason = "")
    {
      this.ClaimsPrincipal = principal;
      this.FailureReason = failureReason;
    }    

    /// <summary>
    /// Constructs a failed authorization with given reason.
    /// </summary>
    /// <param name="failureReason">
    /// Describes the reason for the authorization failure.
    /// </param>
    public ApiAuthorizationResult(string failureReason)
    {
      FailureReason = failureReason;
    }    
  }
}
