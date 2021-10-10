using System;
using System.Collections.Generic;

namespace Server.Security
{
    public class SecurityConfiguration
    {
        public const string Identifier = "Security";

        public SecurityConfiguration()
        {
            // This should only be done once per application run since client JWT data is depending on this key.
            // It resides here for simplicity, in a real system it should probably be located elsewhere to avoid
            // creating new keys when for example hot-reloading config from whatever config source we use.
            // Unless of course we want to load the encryption key from said config as well, in that case it should be read, not randomly generated.
            JwtTokenSecretBase64 = CreateJwtTokenEncryptionKey();
        }

        public string JwtTokenSecretBase64 { get; }
        public int TokenExpirationInSeconds { get; set; } = 60;
        public List<User> Users { get; set; } = new List<User>();

        private static string CreateJwtTokenEncryptionKey()
        {
            var jwtTokenEncryptionKey = new byte[128];
            new Random().NextBytes(jwtTokenEncryptionKey);
            var base64Key = Convert.ToBase64String(jwtTokenEncryptionKey);

            return base64Key;
        }
    }
}
