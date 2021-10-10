using Server.Models;
using HotChocolate.Types;
using System.Linq;
using Server.Repository;
using System.Collections.Generic;
using HotChocolate;

namespace Server.GraphQL
{
    public class BookType : ObjectType<Book>
    {
        private static readonly List<Author> EmptyAuthors = new();

        protected override void Configure(IObjectTypeDescriptor<Book> descriptor)
        {
            descriptor.Description("A book, may be good or bad.");
            descriptor.BindFieldsExplicitly();

            descriptor
                .Field(n => n.Id)
                .Name("id")
                .Description("Book identifier");

            descriptor
                .Field(n => n.Name)
                .Name("name")
                .Description("Name of the book");

            descriptor
                .Field(n => n.Authors)
                .Type<NonNullType<ListType<NonNullType<BookType>>>>()
                .ResolveWith<Resolvers>(n => Resolvers.GetAuthors(default!, default!))
                .Description("All authors who wrote this book");
        }

        private class Resolvers
        {
            public static IEnumerable<Author> GetAuthors(Book book, [Service] IInMemDataRepo repo)
                => repo.Books.FirstOrDefault(b => b.Id == book.Id)?.Authors ?? EmptyAuthors;
        }
    }
}
