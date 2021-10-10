using Server.Models;
using HotChocolate;
using System.Threading;
using Server.Repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Server.GraphQL
{
    public class Mutations
    {
        public AddAuthorResult AddAuthor(AddAuthorInput input, [Service] IInMemDataRepo repo, CancellationToken cs)
        {
            var author = new Author
            {
                Id = repo.GetNextAuthorId(),
                Name = input.name,
                Nickname = input.nickname
            };

            repo.Authors.Add(author);

            return new AddAuthorResult(author);
        }

        public AddBookResult AddBook(AddBookInput input, [Service] IInMemDataRepo repo, CancellationToken cs)
        {
            var book = new Book
            {
                Id = repo.GetNextBookId(),
                Name = input.name,
                Authors = repo.LookupAuthors(input.authorIds).ToList()
                //AuthorId = input.authorId
            };

            // Todo: Lookup books, connect etc.
            repo.Books.Add(book);

            return new AddBookResult(book);
        }
    }

    public record AddAuthorInput(string name, string nickname);
    public record AddAuthorResult(Author author);

    public record AddBookInput(string name, List<int> authorIds);
    public record AddBookResult(Book book);
}
