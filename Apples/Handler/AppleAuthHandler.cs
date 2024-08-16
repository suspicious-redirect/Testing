using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Apple.Data;
using System.Net.Http.Headers;
using System.Text;
using System.Security.Claims;

namespace Apple.Handler
{
    ////////////////////////////////////////////////////// DOCUMENT THIS
    public class AppleAuthHandler : AuthenticationHandler<AuthenticationSchemeOptions>
    {
        private readonly IAppleRepo _repo;
        public AppleAuthHandler(
            IAppleRepo repo, 
            IOptionsMonitor<AuthenticationSchemeOptions> options,
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock)
            : base(options, logger, encoder, clock)
        {
            _repo = repo;
        }

        ////////////////////////////////////////////////////// DOCUMENT THIS
        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            if (!Request.Headers.ContainsKey("Authorization"))
            {
                Response.Headers.Add("www-authenticate", "Basic ");
                return AuthenticateResult.Fail("Authorization header not found.");
            }
            else
            {
                var authHeader = AuthenticationHeaderValue.Parse(Request.Headers["Authorization"]);
                var credentialBytes = Convert.FromBase64String(authHeader.Parameter);
                var credentials = Encoding.UTF8.GetString(credentialBytes).Split(":");
                var username = credentials[0];
                var password = credentials[1];

                if (_repo.ValidUser(username, password))
                {
                    var claims = new[] { new Claim("userName", username) };

                    ClaimsIdentity identity = new ClaimsIdentity(claims, "Basic");
                    ClaimsPrincipal principal = new ClaimsPrincipal(identity);

                    AuthenticationTicket ticket = new AuthenticationTicket(principal, Scheme.Name);

                    return AuthenticateResult.Success(ticket);
                }
                else
                    Response.Headers.Add("www-authenticate", "Basic ");
                    return AuthenticateResult.Fail("userName and password do not match");
            }
        }
    }
}