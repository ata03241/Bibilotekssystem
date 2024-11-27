using System;
using System.Collections.Generic;

namespace Bibliotekssystem.Models
{
    public class Author
    {
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }

 
        public DateTime BirthYear { get; set; }

        public string Biography { get; set; }

        public ICollection<BookAuthor> BookAuthors { get; set; }

    }
}
