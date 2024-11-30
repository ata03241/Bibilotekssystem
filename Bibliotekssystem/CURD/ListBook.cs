using Bibliotekssystem.Models;
using System;
using Microsoft.EntityFrameworkCore;

public class ListBook
{
    public static void Run()
    {
        using(var context = new AppDbContext())
        {
            var books = context.Books.Include(b => b.BookAuthors)
                .ThenInclude(BookAuthor => BookAuthor.Author)
                .ToList();
            
            foreach (var book in books)
            {
                System.Console.WriteLine($"\nBook: {book.Title}");
                foreach (var author in book.BookAuthors)
                {
                    System.Console.WriteLine($"Author: {author.Author.FirstName} {author.Author.LastName}");
                    System.Console.WriteLine($"Bio: {author.Author.Biography}\n");
                }
            }
        }
    }
}