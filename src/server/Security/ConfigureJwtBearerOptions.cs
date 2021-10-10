using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Server.Security
{
    /// <summary>
    /// Configure JwtBearerOptions - this construction allows dependency injection to be used to retrieve e.g. encryption key data from options
    /// See: https://github.com/dotnet/aspnetcore/issues/21491
    /// </summary>
    public class ConfigureJwtBearerOptions : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly SecurityConfiguration _config;

        public ConfigureJwtBearerOptions(IOptions<SecurityConfiguration> config)
        {
            _config = config.Value;
        }

        /// <inheritdoc />
        public void Configure(JwtBearerOptions x)
        {
            throw new InvalidOperationException();
        }

        /// <inheritdoc />
        public void Configure(string name, JwtBearerOptions options)
        {
            options.RequireHttpsMetadata = true;
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(_config.JwtTokenSecretBase64)),

                RequireExpirationTime = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero, // Default is 5 minutes

                ValidateIssuer = false,
                ValidateAudience = false
            };
        }
    }
}