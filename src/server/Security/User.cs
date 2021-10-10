using System.Collections.Generic;

namespace Server.Security
{
    public class User
    {
        public string Name { get; set; }
        public string ApiKey { get; set; }
        public List<UserRole> Roles { get; set; } = new List<UserRole>();
    }
}
