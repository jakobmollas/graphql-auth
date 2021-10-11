using System.Collections.Generic;

namespace Server.Models
{
    public sealed record Author(int Id, string Name, string Nickname = "")
    {
        public List<Book> Books { get; } = new List<Book>();
    }
}
