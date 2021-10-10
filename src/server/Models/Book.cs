using System.Collections.Generic;

namespace Server.Models
{
    public sealed class Book
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public ICollection<Author> Authors { get; init; } = new List<Author>();
    }
}
