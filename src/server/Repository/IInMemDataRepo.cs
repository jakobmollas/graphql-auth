using Server.Models;
using System.Collections.Generic;

namespace Server.Repository
{
    public interface IInMemDataRepo
    {
        List<Author> Authors { get; }
        List<Book> Books { get; }
    }
}