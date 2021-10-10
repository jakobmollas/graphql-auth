using Server.Models;
using HotChocolate;
using Server.Repository;
using System.Collections.Generic;
using HotChocolate.Data;

namespace Server.GraphQL
{
    
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Get authors")]
        public IList<Author> GetAuthors([Service] IInMemDataRepo repo)
        {
            return repo.Authors;
        }

        [UseFiltering]
        [UseSorting]
        [GraphQLDescription("Get books")]
        public IList<Book> GetBooks([Service] IInMemDataRepo repo)
        {
            return repo.Books;
        }
    }
}
