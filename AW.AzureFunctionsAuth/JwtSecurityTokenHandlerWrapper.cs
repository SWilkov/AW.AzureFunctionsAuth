using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using AW.AzureFunctionsAuth.Interfaces;

namespace AW.AzureFunctionsAuth
{
    public class JwtSecurityTokenHandlerWrapper : IJwtSecurityTokenHandlerWrapper
    {
        /// <summary>
        /// Reads and validates a 'JSON Web Token' (JWT) and throws an exception if
        /// the token could not be validated.
        /// </summary>
        /// <param name="token">
        /// A JSON Web Token (JWT) encoded as a JWS or JWE in Compact Serialized Format.
        /// </param>
        /// <param name="tokenValidationParameters">
        /// Contains parameters used in the validation of the token.
        /// </param>
        /// <returns>
        /// A ClaimsPrincipal
        /// </returns>
        public ClaimsPrincipal ValidateToken(
            string token,
            TokenValidationParameters tokenValidationParameters)
        {
            var handler = new JwtSecurityTokenHandler();

            // Try to validate the token.
            // Throws if the the token cannot be validated.            
            return handler.ValidateToken(
                token,
                tokenValidationParameters,
                out _); // Discard the output SecurityToken. We don't need it.
        }
    }
}
