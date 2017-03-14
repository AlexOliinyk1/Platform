using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.Owin.Security.OAuth;
using Platform.Core.Services;

namespace Platform.API.Providers
{
    public class PlatformAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
        {
            private IAuthService _authService;

            public SimpleAuthorizationServerProvider(IAuthService auth)
            {
                _authService = auth;
            }

            public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
            {
                context.Validated();
            }

            public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
            {
                context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });
                bool isExist = await _authService.IsUserExist(context.UserName, context.Password);

                if (!isExist)
                {
                    context.SetError("invalid_grant", "The user name or password is incorrect.");
                    return;
                }

                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);

            }
        }
    }
}