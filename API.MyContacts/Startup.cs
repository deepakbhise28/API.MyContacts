using API.MyContacts.Models;
using API.MyContacts.Provider;
using Microsoft.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler.Encoder;
using Microsoft.Owin.Security.Jwt;
using Microsoft.Owin.Security.OAuth;
using Owin;
using System;
using System.Threading.Tasks;

namespace API.MyContacts
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);

            app.UseCors(Microsoft.Owin.Cors.CorsOptions.AllowAll);
        }     

        /// <summary>
        /// Configure OATH
        /// </summary>
        /// <param name="app"></param>
        public void ConfigureOAuth(IAppBuilder app)
        {
            var issuer = "mycontact";
            var audience = "099153c2625149bc8ecb3e85e03f0022";
            var secret = TextEncodings.Base64Url.Decode("IxrAjDoa2FqElO7IhrSrUJELhUckePEPVpaePlS_Xaw");

            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            OAuthAuthorizationServerOptions OAuthServerOptions = new OAuthAuthorizationServerOptions()
            {
                //For Dev enviroment only (on production should be AllowInsecureHttp = false)
                AllowInsecureHttp = true,
                TokenEndpointPath = new PathString("/token"),
                AccessTokenExpireTimeSpan = TimeSpan.FromMinutes(30),
                Provider = new CustomOAuthProvider(),
                AccessTokenFormat = new CustomJwtFormat("mycontact")
            };

            // OAuth 2.0 Bearer Access Token Generation
            app.UseOAuthAuthorizationServer(OAuthServerOptions);

            // Api controllers with an [Authorize] attribute will be validated with JWT
            app.UseJwtBearerAuthentication(
                new JwtBearerAuthenticationOptions
                {
                    AuthenticationMode = AuthenticationMode.Active,
                    AllowedAudiences = new[] { audience },
                    IssuerSecurityKeyProviders = new[]
                    {
                       new SymmetricKeyIssuerSecurityKeyProvider(issuer, secret)
                    },

                    Provider = new OAuthBearerAuthenticationProvider
                    {
                        OnValidateIdentity = context =>
                        {                           
                            return Task.FromResult<object>(null);
                        }
                    }                   
                });
        }
    }
}
