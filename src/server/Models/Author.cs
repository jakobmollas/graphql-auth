using HotChocolate;
using System.Collections.Generic;

namespace Server.Models
{
    public sealed class Author
    {
        public int Id { get; set; }

        [GraphQLNonNullType]
        public string Name { get; set; }

        public string Nickname { get; set; }

        [GraphQLNonNullType]
        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
