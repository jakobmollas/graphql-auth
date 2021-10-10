using System.Collections.Generic;

namespace Server.Models
{
    public sealed class Author
    {
        public int Id { get; init; }

        public string Name { get; init; } = "";

        public string Nickname { get; init; } = "";

        public ICollection<Book> Books { get; init; } = new List<Book>();
    }
}
