using Bibliotekssystem.Models;
using System;

public class AddbookAuth
{
    public static void Run()
    {
         using (var context = new AppDbContext())
        {
            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return;
            }

            Console.Write("Enter Author ID: ");
            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Invalid Author ID.");
                return;
            }

            var bookAuthor = new BookAuthor
            {
                BookID = bookId,
                AuthorId = authorId
            };

            context.BookAuthors.Add(bookAuthor);
            context.SaveChanges();
            Console.WriteLine($"Relationship added between Book {bookId} and Author {authorId}.");
        }
    }
}