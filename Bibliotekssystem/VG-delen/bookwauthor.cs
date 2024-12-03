using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class bookwauthor
{
    public static void Run()
    {
        System.Console.Write("Book name: ");
        var input = Console.ReadLine();

        using (var context = new AppDbContext())
        {
            var authors = context.Books.Include(b => b.BookAuthors)
                .ThenInclude(b => b.Author)
                .Where(B => B.Title.Contains(input))
                .SelectMany(b => b.BookAuthors.Select(b => b.Author))
                .ToList();

            if (!authors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"No authors found for the book '{input}'.");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                Console.WriteLine($"\nAuthors for the book '{input}':");
                foreach (var author in authors)
                {
                    Console.WriteLine($"Author name:  {author.FirstName} {author.LastName}");
                }
            }
        }
    }
}