using System;
using System.Security.Cryptography;
using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;


class Program
{
    static void Main(string[] args)
    {
        Seed.Run();
        System.Console.WriteLine("\n Welcome to library");
        int menuSel = 13;
        do
        {
            menuSel = MenuSelection();
            MenuExecution(menuSel);

        } while (menuSel != 13);
        Console.ForegroundColor = ConsoleColor.DarkYellow;
        System.Console.WriteLine("\nThank you for your time");
        Console.ForegroundColor = ConsoleColor.White;
        
    }

    private static void MenuExecution(int menuSel)
    {
        switch (menuSel)
        {
            case 1:
                AddAuthorr.Run();
                break;
            case 2:
                AddBookss.Run();
                break;
            case 3:
                AddbookAuth.Run();
                break;
            case 4:
                Addloans.Run();
                break;
            case 5:
                ReturnBook.Run();
                break;
            case 6:
                Remove.Run();
                break;
            case 7:
                ListBook.Run();
                break;
            case 8:
                ListLoan.Run();
                break;
            case 9:
                Update.Run();
                break;
            case 10:
                authorwbook.Run();
                break;
            case 11:
                bookwauthor.Run();
                break;
            case 12: 
                loanhistory.Run();
                break;

        }
    }

    private static int MenuSelection()
    {
        int menuSel = 13;
        while (true)
        {
            try
            {
                for (int i = 0; i < menuSel; i++)
                {
                    System.Console.WriteLine("\nWelcome to library");
                    System.Console.WriteLine("1. Create a new author");
                    System.Console.WriteLine("2. Create a new Book");
                    System.Console.WriteLine("3. Add a relationship between book and an author");
                    System.Console.WriteLine("4. Add a loan for a book");
                    System.Console.WriteLine("5. Return book");
                    System.Console.WriteLine("6. Remove author, books and loans");
                    System.Console.WriteLine("7. List Books With Authors ");
                    System.Console.WriteLine("8. List all loan book");
                    System.Console.WriteLine("9. Update author, book and relationship");
                    System.Console.WriteLine("10. List all books by a specific author");
                    System.Console.WriteLine("11. List all authors of a specific book");
                    System.Console.WriteLine("12. Show loan history");
                    System.Console.WriteLine("13. Quit");
                     menuSel = int.Parse(Console.ReadLine());
                    if (menuSel >= 1 && menuSel <= 13)
                    {
                        break;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Invalid selection");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Wrong selection, pls try again");
                Console.ForegroundColor = ConsoleColor.White;
                continue;
            }
            return menuSel;
        }

    }
}

