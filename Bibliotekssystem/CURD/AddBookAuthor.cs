using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class AddbookAuth
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            //lista ut alla books
            Console.WriteLine("Available Books:");
            var books = context.Books.ToList();
            if (!books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No books found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var book in books)
                {
                    Console.WriteLine($"Book ID: {book.Id}, Title: {book.Title}");
                }
            }

            Console.WriteLine(); 

            // lista ut alla authors
            Console.WriteLine("Available Authors:");
            var authors = context.Authors.ToList();
            if (!authors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No authors found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var author in authors)
                {
                    Console.WriteLine($"Author ID: {author.Id}, Name: {author.FirstName} {author.LastName}");
                }
            }

            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Book ID.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Enter Author ID: ");
            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Author ID.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var bookAuthor = new BookAuthor
            {
                BookID = bookId,
                AuthorId = authorId
            };

            context.BookAuthors.Add(bookAuthor);
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Relationship added between Book {bookId} and Author {authorId}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}