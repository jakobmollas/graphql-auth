using Server.Models;
using HotChocolate;
using Server.Repository;
using System.Collections.Generic;
using HotChocolate.AspNetCore.Authorization;
using Server.Security;

namespace Server.GraphQL
{
    public class Mutations
    {
        [Authorize(Roles = new[] { UserRoles.Write })]
        public AddAuthorResult AddAuthor(AddAuthorInput input, [Service] IInMemDataRepo repo)
        {
            var author = new Author(
                Id: repo.GetNextAuthorId(),
                Name: input.Name,
                Nickname: input.Nickname);

            repo.Authors.Add(author);

            return new AddAuthorResult(author);
        }

        public AddBookResult AddBook(AddBookInput input, [Service] IInMemDataRepo repo)
        {
            var book = new Book(repo.GetNextBookId(), input.Name);
            foreach (var author in repo.LookupAuthors(input.AuthorIds))
                book.Authors.Add(author);

            repo.Books.Add(book);

            return new AddBookResult(book);
        }
    }

    public record AddAuthorInput(string Name, string Nickname);
    public record AddAuthorResult(Author Author);

    public record AddBookInput(string Name, IReadOnlyList<int> AuthorIds);
    public record AddBookResult(Book Book);
}
