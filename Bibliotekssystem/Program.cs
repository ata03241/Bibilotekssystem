using System;
using System.Security.Cryptography;
using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;


class Program
{
    static void Main(string[] args)
    {
        Seed.Run();
        System.Console.WriteLine("\nWelcome to library");
        System.Console.WriteLine("1. Create a new author");
        System.Console.WriteLine("2. Create a new Book");
        System.Console.WriteLine("3. Add a relationship between book and an author");
        System.Console.WriteLine("4. Add a loan for a book");
        System.Console.WriteLine("5. Return book");
        System.Console.WriteLine("6. Remove author, books and loans");
        System.Console.WriteLine("7. List Books WithAuthors ");
        System.Console.WriteLine("8. List all loan book");

        var input = Console.ReadLine();

        switch (input)
        {
            case "1":
                AddAuthorr.Run();
                break;
            case "2":
                AddBookss.Run();
                break;
            case "3":
                AddbookAuth.Run();
                break;
            case "4":
                Addloans.Run();
                break;
            case "5":
                ReturnBook.Run();
                break;
            case "6":
                // Removeabl(); //kvar att fixa
                break;
            case "7":
                ListBook.Run();
                break;
            case "8":
                ListLoan.Run();
                break;

        }
    }
}

