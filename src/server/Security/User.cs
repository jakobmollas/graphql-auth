using System.Collections.Generic;

namespace Server.Security
{
    public class User
    {
        public string Name { get; init; } = "";
        public string ApiKey { get; init; } = "";
        public List<string> Roles { get; } = new List<string>();
    }
}
