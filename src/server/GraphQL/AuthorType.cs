using Server.Models;
using HotChocolate.Types;
using HotChocolate;
using Server.Repository;
using System.Linq;
using System.Collections.Generic;

namespace Server.GraphQL
{
    public class AuthorType : ObjectType<Author>
    {
        protected override void Configure(IObjectTypeDescriptor<Author> descriptor)
        {
            descriptor.Description("Author of one or more books.");

            // If we do not want all (public get) properties of the base/generic type to be exposed in schema 
            // we need to instruct the descriptor to bind fields explicitly - default is to bind fields implicitly/automatically
            // This will prevent accidental exposure of any new fields on the generic type and force all schema fields to be explicitly defined here.
            descriptor.BindFieldsExplicitly();

            // Note: Names and types are automatically inferred from generic type properties, 
            //       they are explicitly set here to show how to do it and to avoid accidental updates due changes in underlying types
            descriptor
                .Field(n => n.Id)
                .Name("id")
                .Description("Author identifier");

            descriptor
                .Field(n => n.Name)
                .Name("name")
                .Type<NonNullType<StringType>>()
                .Description("Full author name");

            descriptor
                .Field(n => n.Nickname)
                .Name("nickname")
                .Type<StringType>()
                .Description("Optional nickname");

            // There are several ways to resolve data, sometimes it can be done implicitly, 
            // for example simple properties are mapped directly by HotChocolate
            // and more complex ones may need explicit resolvers of varying complexity, see below.

            // 1. Implicit Projections
            // Enable UseProjections (in configure services) and get data directly from model, given that models actually contain that data

            // 2. External resolvers
            // Note: There may be a bug in newer versions of HotChocolate where it seems to add a required argument "author" (or whatever the resolver parameter is called) to the "books" field in the graphql definition for some reason, 
            //       this does not happen in 11.x.y versions
            descriptor
                .Field(n => n.Books)
                .Name("books")
                .Type<NonNullType<ListType<NonNullType<BookType>>>>()
                .ResolveWith<Resolvers>(n => Resolvers.GetBooks(default!, default!))
                .Description("All books written by this author.");

            // 3. Inline resolvers
            //descriptor
            //    .Field(n => n.Books)
            //    .Description("All books written by this author.")
            //    .Resolve(context =>
            //    {
            //        Author author = context.Parent<Author>();
            //        var repo = context.Service<IInMemDataRepo>();

            //        return repo.Books.Where(b => b.Authors.Any(a => a.Id == author.Id));
            //    });
        }

        private class Resolvers
        {
            public static IEnumerable<Book> GetBooks(Author author, [Service] IInMemDataRepo repo)
                => repo.Books.Where(b => b.Authors.Any(a => a.Id == author.Id));
        }
    }
}
