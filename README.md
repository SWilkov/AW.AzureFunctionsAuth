# AW.AzureFunctionsAuth

An update to the existing excellent repository [AzureFunctionsOpenIDConnectAuthSample](https://github.com/bryanknox/AzureFunctionsOpenIDConnectAuthSample) to protect access to Azure Functions API triggered by HTTP that supports OpenID Connect protocols ie [Auth0](https://auth0.com/). 

## AW.AzureFunctionsAuth uses
* C#
* .Net 6
* Azure Functions 4 - isolated process
* Dependency Injection
* Options for App Settings
* OpenIDConnect (OIDC) plus Bearer Tokens

## Notes
Added an optional `ClaimsApiPermissionService` to validate User Claims against expected permissions.

The code is near identical except the top level `OidcApiAuthorizationService` expects a paramater of type *HttpHeaderCollection* rather than a *HeaderDictionary* as per isolated Azure Functions in .Net 5 and 6.

Have also added an optional `ClaimsPrincipal` to the `ApiAuthorizationResult` object
