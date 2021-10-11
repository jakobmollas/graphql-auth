using System.Collections.Generic;

namespace Server.Models
{
    public sealed record Book(int Id, string Name)
    {
        public List<Author> Authors { get; } = new List<Author>();
    }
}
