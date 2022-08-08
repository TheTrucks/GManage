using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using System.Diagnostics.CodeAnalysis;
using System.Text.Encodings.Web;

namespace GManage.System
{
    public class GManAuthFilter : AuthenticationHandler<GManAuthOptions>
    {
        private readonly int _minsBeforeExpired;
        private readonly Models.IAuthenticationWorker _authWorker;

        public GManAuthFilter
        (
            IOptionsMonitor<GManAuthOptions> options, 
            ILoggerFactory logger,
            UrlEncoder encoder,
            ISystemClock clock,
            Models.IAuthenticationWorker authWorker) : base(options, logger, encoder, clock)
        {
            _minsBeforeExpired = Options.ExpiresIn > 0
                ? Options.ExpiresIn
                : 60;
            _authWorker = authWorker;
        }

        protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            return AuthenticateResult.Fail("lmao");
        }
    }

    public class GManAuthOptions : AuthenticationSchemeOptions
    {
        public int ExpiresIn { get; set; }
    }
}
