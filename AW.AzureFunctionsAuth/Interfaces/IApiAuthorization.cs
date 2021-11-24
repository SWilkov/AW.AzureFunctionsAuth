using Microsoft.Azure.Functions.Worker.Http;
using AW.AzureFunctionsAuth.Models;

namespace AW.AzureFunctionsAuth.Interfaces
{
  public interface IApiAuthorization
  {
    Task<ApiAuthorizationResult> AuthorizeAsync(HttpHeadersCollection httpRequestHeaders);
    Task<HealthCheckResult> HealthCheckAsync();    
  }
}
