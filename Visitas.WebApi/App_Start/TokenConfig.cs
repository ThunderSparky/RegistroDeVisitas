using Microsoft.Owin;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using Visitas.UnitOfWork;

namespace Visitas.WebApi.App_Start
{
    public static class TokenConfig
    {
        public static void ConfigureOAuth(IAppBuilder app, HttpConfiguration config)
        {
            var unitOfWork = (IUnitOfWork)config.DependencyResolver.GetService(typeof(IUnitOfWork));

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromDays(1),
                Provider = new SimpleAuthorizationServerProvider(unitOfWork)
            };
            app.UseOAuthAuthorizationServer(OAuthServerOptions);
            app.UseOAuthBearerAuthentication(new OAuthBearerAuthenticationOptions());
        }
    }

    public class SimpleAuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        private readonly IUnitOfWork _unit;
        public SimpleAuthorizationServerProvider(IUnitOfWork unit)
        {
            _unit = unit;
        }
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            await Task.Factory.StartNew(() => { context.Validated(); });
        }
        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            await Task.Factory.StartNew(() =>
            {
                var user = _unit.Users.ValidateUser(context.UserName, context.Password);
                if (user == null)
                {
                    context.SetError("invalid_grant", "Usuario o Password incorrecto");
                    return;
                }
                var identity = new ClaimsIdentity(context.Options.AuthenticationType);
                identity.AddClaim(new Claim("sub", context.UserName));
                identity.AddClaim(new Claim("role", "user"));

                context.Validated(identity);
            });
        }
    }
}