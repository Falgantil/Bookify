using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Bookify.Core;

namespace Bookify.DataAccess.Services
{
    public class AuthenticationService : IAuthenticationService
    {
        private BookifyContext _bookifyContext;

        public AuthenticationService(BookifyContext bookifyContext)
        {
            _bookifyContext = bookifyContext;
        }

        private const string SecretKey = "yuoypr3QeRZkwGcfj24y4XGODwnkXOy1";

        public async Task<string> CreateAuthToken(string username, string password)
        {
            //_bookifyContext.

            var now = DateTimeOffset.Now.ToUniversalTime();
            var unixNow = now.ToUnixTimeSeconds();
            var unixExpired = now.AddYears(1).ToUnixTimeSeconds();
            var payload = new Dictionary<string, object>()
            {
                { "issdate", unixNow },
                { "expdate", unixExpired },
                { "userid", 5 }
            };
            string token = JWT.JsonWebToken.Encode(payload, SecretKey, JWT.JwtHashAlgorithm.HS256);
            return token;
        }
    }
}