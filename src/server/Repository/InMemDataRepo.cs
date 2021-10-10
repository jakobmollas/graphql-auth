using Server.Models;
using System.Collections.Generic;

namespace Server.Repository
{
    public sealed class InMemDataRepo : IInMemDataRepo
    {
        public List<Author> Authors { get; private set; } = new List<Author>();
        public List<Book> Books { get; private set; } = new List<Book>();

        public InMemDataRepo()
        {
            Init();
        }

        private void Init()
        {
            var a1 = new Author { Id = 1, Name = "Jon Skeet", Nickname = "Shieet" };
            var a2 = new Author { Id = 2, Name = "God", Nickname = "Almighty" };
            var a3 = new Author { Id = 3, Name = "Leif C" };

            var b1 = new Book { Id = 1, Name = "C# In Depth 2nd Edition", Authors = new[] { a1 } };
            var b2 = new Book { Id = 2, Name = "C# In Depth 3rd Edition", Authors = new[] { a1 } };
            var b3 = new Book { Id = 3, Name = "Old Testament", Authors = new[] { a2 } };
            var b4 = new Book { Id = 4, Name = "New Testament", Authors = new[] { a2 } };
            var b5 = new Book { Id = 5, Name = "Jag fattar ingenting", Authors = new[] { a2, a3 } };
            var b6 = new Book { Id = 6, Name = "Jag fattar fortfarande ingenting", Authors = new[] { a2, a3 } };
            var b7 = new Book();

            a1.Books.Add(b1);
            a1.Books.Add(b2);

            a2.Books.Add(b3);
            a2.Books.Add(b4);
            a2.Books.Add(b5);
            a2.Books.Add(b6);

            a3.Books.Add(b5); 
            a3.Books.Add(b6);

            Authors = new List<Author> { a1, a2, a3 };
            Books = new List<Book> { b1, b2, b3, b4, b5, b6 };
        }
    }
}
