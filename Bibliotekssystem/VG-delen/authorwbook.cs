using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class authorwbook
{
    public static void Run()
    {
        System.Console.Write("Author first name: ");
        var input = Console.ReadLine();
        using (var context = new AppDbContext())
        {
        var authorb = context.Authors.Include(a => a.BookAuthors)
            .ThenInclude(a => a.Book)
            .Where(a => a.FirstName.Contains(input)) //filtrera firstname med where
            .SelectMany(a => a.BookAuthors.Select(bo => bo.Book)) //för att ta ut alla böcker från relation
            .ToList();
        
        if(!authorb.Any())
        {
            Console.ForegroundColor = ConsoleColor.Red;
            System.Console.WriteLine($"No books found for the author {input}");
            Console.ForegroundColor = ConsoleColor.White;
        }
        else
        {
            System.Console.WriteLine($"Books written by {input}");
            foreach (var book in authorb)
            {
                System.Console.WriteLine($"book title: {book.Title}");
            }
        }          

        }
    }
}