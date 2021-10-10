using Server.Models;
using HotChocolate;
using System.Threading;
using Server.Repository;

namespace Server.GraphQL
{
    // This is not fully implemented yet
    public class Mutations
    {
        public AddAuthorResult AddAuthor(AddAuthorInput input, [Service] IInMemDataRepo repo, CancellationToken cs)
        {
            var author = new Author
            {
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
                Name = input.name,
                //AuthorId = input.authorId
            };

            // Todo: Lookup books, connect etc.
            repo.Books.Add(book);

            return new AddBookResult(book);
        }
    }

    public record AddAuthorInput(string name, string nickname);
    public record AddAuthorResult(Author author);

    public record AddBookInput(string name);
    public record AddBookResult(Book book);
}
