using Bibliotekssystem.Models;
using System;
using Microsoft.EntityFrameworkCore;

public class Seed
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            if(!context.Authors.Any())
            {
                var author1 = new Author
                {
                    FirstName = "NIKO",
                    LastName = "BOY",
                    BirthYear = new DateTime(1989,7,10),
                    Biography = "Young boy from Thailand"
                };

                var author2 = new Author()
                {
                    FirstName = "Joar",
                    LastName = "Boy",
                    BirthYear = new DateTime (1999, 12, 17),
                    Biography = "Who knows"
                };
                context.Authors.Add(author1);
                context.Authors.Add(author2);
                context.SaveChanges();
                Console.WriteLine("Seed data for authors added successfully.");
            }
            else
            {
                Console.WriteLine("Authors already exist in the database.");
            }

            if(!context.Books.Any())
            {
                var book1 = new Book
                {
                    Title = "The mountain",
                    ReleaseDate = new DateTime(2011,1,1),
                    Summary = "In The Mountains Whisper the story unfolds in a remote village of Nepal, where the protagonist embarks on a journey to uncover his family's hidden past.",
                    Publisher = "John cena",
                    IsAvailable = true
                };

                var book2 = new Book
                {
                    Title = "The City of Dreams",
                    ReleaseDate = new DateTime(2018, 11, 5),
                    Summary = "The City of Dreams' tells the story of a young woman from a rural town who moves to Kathmandu with dreams of success. The novel captures the struggles of adapting to a fast-paced urban life, while navigating love, ambition, and cultural challenges.",
                    Publisher = "Ashok",
                    IsAvailable = true
                };
                
                context.Books.Add(book1);
                context.Books.Add(book2);
                context.SaveChanges();
                Console.WriteLine("Books have been added successfully.");
            }

            if (!context.BookAuthors.Any())
            {
                var book1 = context.Books.First(b => b.Title == "The mountain");
                var author1 = context.Authors.First(a => a.FirstName == "NIKO" && a.LastName == "BOY");
 
                var book2 = context.Books.First(b => b.Title == "The City of Dreams");
                var author2 = context.Authors.First(a => a.FirstName == "Joar" && a.LastName == "Boy");
 
                context.BookAuthors.Add(new BookAuthor { BookID = book1.Id, AuthorId = author1.Id });
                context.BookAuthors.Add(new BookAuthor { BookID = book2.Id, AuthorId = author2.Id });
 
                context.SaveChanges();
                Console.WriteLine("Book-author relationships have been added successfully.");
            }
 
        }
    }
}