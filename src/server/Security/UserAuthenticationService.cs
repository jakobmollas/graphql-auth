using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Server.Security
{
    public interface IUserAuthenticationService
    {
        string? Authenticate(string apiKey);
    }

    /// <summary>
    /// Handles authentication of users/users api keys by trying to match incoming api key data to our user repository.
    /// Creates and returns JWT data on successful matches.
    /// </summary>
    public sealed class UserAuthenticationService : IUserAuthenticationService
    {
        private readonly SecurityConfiguration _config;
        private readonly SigningCredentials _signingCredentials;

        public UserAuthenticationService(IOptions<SecurityConfiguration> config)
        {
            _config = config.Value;

            _signingCredentials = new SigningCredentials(
                key: new SymmetricSecurityKey(Convert.FromBase64String(_config.JwtTokenSecretBase64)),
                algorithm: SecurityAlgorithms.HmacSha256Signature);
        }

        public string? Authenticate(string apiKey)
        {
            // Case-sensitive check
            var user = _config.Users.FirstOrDefault(n => string.Equals(n.ApiKey, apiKey, StringComparison.Ordinal));

            if (user == null)
                return null;

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, user.Name) };
            claims.AddRange(user.Roles.Select(role => new Claim(ClaimTypes.Role, role)));

            // For more info on JWT, see:
            // https://jwt.io/

            var jwt = new JwtSecurityToken(
                issuer: "ExampleCorp",      // Only used if TokenValidationParameters.ValidateIssuer is enabled 
                audience: "Everyone",       // Only used if TokenValidationParameters.ValidateAudience is enabled 

                // Claims, and all parts of a token, are visible to all that have access to the token unless we also encrypt the token (which should be avoided)
                claims: claims,

                // Set expiration time - too short and too many calls will need to be re-authenticated, too few and tokens will never expire
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddSeconds(_config.TokenExpirationInSeconds),

                // Signing token - tokens should ALWAYS be signed by us in order to detect tampering 
                signingCredentials: _signingCredentials);

            var token = new JwtSecurityTokenHandler().WriteToken(jwt);

            return token;
        }
    }
}
