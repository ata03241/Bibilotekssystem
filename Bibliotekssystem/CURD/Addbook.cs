using Bibliotekssystem.Models;
using System;

public class AddBookss
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Enter Book name: ");
            var bookName = Console.ReadLine();

            Console.WriteLine("Enter Book ReleaseDate (yyyy-MM-dd): ");
            var bookReleaseDate = Console.ReadLine();
            if (!DateTime.TryParse(bookReleaseDate, out var releaseDate))
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            Console.WriteLine("Enter Book Summary: ");
            var bookSummary = Console.ReadLine();

            Console.WriteLine("Enter Book Publisher: ");
            var publisher = Console.ReadLine();

            Console.WriteLine("Is the book available (yes/no): ");
            var availabilityInput = Console.ReadLine()?.ToLower();
            bool isAvailable = availabilityInput == "yes";


            var book = new Book
            {
                Title = bookName,
                ReleaseDate = releaseDate,
                Summary = bookSummary,
                Publisher = publisher,
                IsAvailable = isAvailable
            };

            context.Books.Add(book);
            context.SaveChanges();

            Console.WriteLine($"Book '{bookName}' added successfully!");
        }
    }
}