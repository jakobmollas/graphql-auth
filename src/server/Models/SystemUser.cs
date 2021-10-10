using System.Collections.Generic;

namespace Server.Models
{
    public sealed class SystemUser
    {
        public string Name { get; }

        public ICollection<string> Roles { get; } = new List<string>();

        public SystemUser(string name)
        {
            Name = name;
        }
    }
}
