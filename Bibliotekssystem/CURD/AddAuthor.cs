using Bibliotekssystem.Models;
using System;

public class AddAuthorr
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            System.Console.Write("Write Author First Name: ");
            var authorName = Console.ReadLine();
            System.Console.Write("Write Author Last Name: ");
            var authorLast = Console.ReadLine();
            Console.Write("Author's Date of Birth (yyyy-MM-dd): ");
            var birthDateInput = Console.ReadLine();

            if (!DateTime.TryParse(birthDateInput, out DateTime birthDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.WriteLine("Write author's Biography: ");
            var biography = Console.ReadLine();

            var author = new Author
            {
                FirstName = authorName,
                LastName = authorLast,
                BirthYear = birthDate,
                Biography = biography
            };


            context.Authors.Add(author);
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Author {authorName} {authorLast} added successfully!");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}