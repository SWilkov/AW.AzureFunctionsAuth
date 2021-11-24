using System.Net.Http.Headers;
using AW.AzureFunctionsAuth.Interfaces;
using Microsoft.Azure.Functions.Worker.Http;

namespace AW.AzureFunctionsAuth
{
  public class AuthorizationHeaderBearerTokenExtractor : IAuthorizationHeaderBearerTokenExtractor
  {
    /// <summary>
    /// Extracts the Bearer token from the Authorization header of the given HTTP request headers.
    /// </summary>
    /// <param name="headers">
    /// The headers from an HTTP request.
    /// </param>
    /// <returns>
    /// The Bearer token extracted from the Authorization header (without the "Bearer " prefix),
    /// or null if the Authorization header was not found, it is in an invalid format,
    /// or its value is not a Bearer token.
    /// </returns>
    public string? GetToken(HttpHeadersCollection httpHeadersCollection)
    {
      if (httpHeadersCollection == null) throw new ArgumentNullException(nameof(httpHeadersCollection));
      //Get a IEnumerable<string> object that represents the content of the Auth header collection
      IEnumerable<string> rawAuthorizationHeaderValue = httpHeadersCollection
        .SingleOrDefault(x => x.Key == "Authorization")
        .Value;
      
      if (rawAuthorizationHeaderValue == null || rawAuthorizationHeaderValue?.Count() != 1)
      {
        // IEnumerable<string> Count will be zero if there is no Authorization header
        // and greater than one if there are more than one Authorization headers.
        return null;
      }

      // We got a value from the Authorization header.
      if (!AuthenticationHeaderValue.TryParse(
              rawAuthorizationHeaderValue.First(), 
              out AuthenticationHeaderValue? authenticationHeaderValue))
      {
        // Invalid token format.
        return null;
      }

      if (!string.Equals(
              authenticationHeaderValue.Scheme,
              "Bearer",
              StringComparison.InvariantCultureIgnoreCase)) // Case insenitive.
      {
        // The Authorization header's value is not a Bearer token.
        return null;
      }

      // Return the token from the Authorization header.
      // This is the token with the "Bearer " prefix removed.
      // The Parameter will be null, if nothing followed the "Bearer " prefix.
      return authenticationHeaderValue.Parameter;
    }
  }
}
