using System.Collections.Generic;

namespace Server.Models
{
    public sealed class Book
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
