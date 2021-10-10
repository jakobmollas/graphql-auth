using HotChocolate;
using System.Collections.Generic;

namespace Server.Models
{
    public sealed class Book
    {
        public int Id { get; set; }

        [GraphQLNonNullType]
        public string Name { get; set; }

        [GraphQLNonNullType]
        public ICollection<Author> Authors { get; set; } = new List<Author>();
    }
}
