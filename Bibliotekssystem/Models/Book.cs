using System.Collections.Generic;

namespace Bibliotekssystem.Models
{
    public class Book
    {
        public int Id {get; set;}
        public string Title{get; set;}
        public DateTime ReleaseDate{get; set;}
        public string? Summary{get; set;}
        public string Publisher{get; set;}
        public bool IsAvailable {get; set;}

        public ICollection<BookAuthor> BookAuthors{get; set;}
        public ICollection<Loan> Loans{get; set;}
    }
}