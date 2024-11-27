using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bibliotekssystem.Models
{
    public class BookAuthor 
    {
        public int Id{get; set;}

        public int BookID{get; set;}
        public Book Book { get; set; }

        public int AuthorId{get; set;}
         public Author Author { get; set;}

    }
}