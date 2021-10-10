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

            descriptor
                .Field(n => n.Authors)
                .ResolveWith<Resolvers>(n => Resolvers.GetAuthors(default!, default!))
                .Description("All authors who wrote this book.");

            // Alternative way
            //descriptor
            //    .Field(n => n.Authors)
            //    .Description("All authors who wrote this book.")
            //    .Resolve(context =>
            //    {
            //        Book book = context.Parent<Book>();
            //        var repo = context.Service<IInMemDataRepo>();

            //        return repo.Books.FirstOrDefault(b => b.Id == book.Id)?.Authors ?? EmptyAuthors;
            //    });
        }

        private class Resolvers
        {
            public static IEnumerable<Author> GetAuthors(Book book, [Service] IInMemDataRepo repo)
                => repo.Books.FirstOrDefault(b => b.Id == book.Id)?.Authors ?? EmptyAuthors;
        }
    }
}
