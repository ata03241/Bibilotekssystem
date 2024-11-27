using System.Collections.Generic;
 
namespace Bibliotekssystem.Models
{
    public class Loan
    {
        public int Id{get; set;}
        public int BookId{get; set;}
        public Book  Book{get; set;}

        public string BorrowerName{get; set;}
        public DateTime LoanDate{get; set;}
        public DateTime? ReturnDate{get; set;}

        public bool IsReturned{get; set;}

    }
}