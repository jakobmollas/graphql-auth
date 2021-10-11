using Server.Models;
using System.Collections.Generic;
using System.Linq;

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
            var a1 = new Author(Id: 1, Name: "Jon Skeet", Nickname: "Shieet");
            var a2 = new Author(Id: 2, Name: "God", Nickname: "Almighty");
            var a3 = new Author(Id: 3, Name: "Leif C");

            var b1 = new Book(Id: 1, Name: "C# In Depth 2nd Edition");
            var b2 = new Book(Id: 2, Name: "C# In Depth 3rd Edition");
            var b3 = new Book(Id: 3, Name: "Old Testament");
            var b4 = new Book(Id: 4, Name: "New Testament");
            var b5 = new Book(Id: 5, Name: "Jag fattar ingenting");
            var b6 = new Book(Id: 6, Name: "Jag fattar fortfarande ingenting");

            a1.Books.AddRange(new[] { b1, b2 });
            a2.Books.AddRange(new[] { b3, b4, b5, b6 });
            a3.Books.AddRange(new[] { b5, b6 });

            b1.Authors.Add(a1);
            b2.Authors.Add(a1);
            b3.Authors.Add(a2);
            b4.Authors.Add(a2);
            b5.Authors.AddRange(new[] { a2, a3 });
            b6.Authors.AddRange(new[] { a2, a3 });

            Authors = new List<Author> { a1, a2, a3 };
            Books = new List<Book> { b1, b2, b3, b4, b5, b6 };
        }

        public int GetNextAuthorId() => Authors.Max(n => n.Id) + 1;

        public int GetNextBookId() => Books.Max(n => n.Id) + 1;

        public IReadOnlyList<Author> LookupAuthors(IReadOnlyList<int> authorIds) =>
            (from id in authorIds
             from author in Authors
             where author.Id == id
             select author)
                    .Distinct()
                    .ToList();
    }
}
