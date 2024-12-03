using Bibliotekssystem.Models;
using System;
using System.Linq;


public class Remove
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            System.Console.WriteLine("\nRemove author book or loan");
            System.Console.WriteLine("1. Remove author");
            System.Console.WriteLine("2. Remove Book");
            System.Console.WriteLine("3. Remove loan");
            System.Console.WriteLine("4. Remove relationship");

            var input = Console.ReadLine();

            switch (input)
            {
                case "1":
                    removeauth();
                    break;
                case "2":
                    Removebook();
                    break;
                case "3":
                    removeloan();
                    break;
                case "4":
                    removerelation();
                    break;
            }
        }
    }

    private static void removerelation()
    {
        using (var context = new AppDbContext())
        {
            ListBook.Run();
            System.Console.WriteLine("Enter BookID to remove relationship: ");
            if (int.TryParse(Console.ReadLine(), out var removeBookId))
            {
                System.Console.WriteLine("Enter AuthorId to remove relationship");
                if (int.TryParse(Console.ReadLine(), out var removeauthid))
                {
                    var bookauth = context.BookAuthors
                        .FirstOrDefault(bauth => bauth.BookID == removeBookId && bauth.AuthorId == removeauthid);

                    if (bookauth != null)
                    {
                        context.BookAuthors.Remove(bookauth);
                        context.SaveChanges();
                        Console.ForegroundColor = ConsoleColor.Green;
                        System.Console.WriteLine($"Relationship between Book {removeBookId} and Author {removeauthid} has beenremoved.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        System.Console.WriteLine("Relationship not found.");
                        Console.ForegroundColor = ConsoleColor.White;
                    }
                }
            }
        }
    }

    private static void removeloan()
    {
        using (var context = new AppDbContext())
        {
            var loans = context.Loans.ToList();
            if (!loans.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("No loan found");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var loa in loans) //lista alla loans
                {
                    System.Console.WriteLine($"loan id: {loa.Id} Borrower name: {loa.BorrowerName}");
                }
            }
            Console.Write("Enter Loan ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out var loanId))
            {
                var loan = context.Loans.Find(loanId);
                if (loan != null)
                {
                    context.Loans.Remove(loan);
                    context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Loan removed successfully.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Loan not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
        }
    }
    private static void Removebook()
    {
        using (var context = new AppDbContext())
        {

            var books = context.Books.ToList();
            if (!books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("No book found");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var boo in books) //lista alla books
                {
                    System.Console.WriteLine($"ID: {boo.Id} title: {boo.Title}");
                }
            }
            System.Console.Write("Enter book id to remove: ");
            if (int.TryParse(Console.ReadLine(), out var bookid))
            {
                var book = context.Books.Find(bookid);
                if (book != null)
                {
                    var bookauth = context.BookAuthors.Where(ba => ba.BookID == bookid).ToList();
                    context.BookAuthors.RemoveRange(bookauth);

                    context.Books.Remove(book);
                    context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    System.Console.WriteLine("Book removed successfully");
                    Console.ForegroundColor = ConsoleColor.White;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Book not found.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }

        }
    }

    private static void removeauth()
    {
        using (var context = new AppDbContext())
        {
            var authors = context.Authors.ToList();

            if (!authors.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("No authors found");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var autho in authors) //lista alla authors
                {
                    System.Console.WriteLine($"ID: {autho.Id} firstname: {autho.FirstName} lastname: {autho.LastName}");
                }
            }

            System.Console.Write("Enter author ID to delete: ");
            if (int.TryParse(Console.ReadLine(), out var authorid))
            {
                var author = context.Authors.Find(authorid);
                if (author != null)
                {
                    var bookauth = context.BookAuthors.Where(b => b.AuthorId == authorid).ToList();
                    context.BookAuthors.RemoveRange(bookauth); //kollar relation innan manm remove

                    context.Authors.Remove(author);
                    context.SaveChanges();
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Author removed successfully.");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }
            else
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Author not found.");
                Console.ForegroundColor = ConsoleColor.White;
            }
        }

    }
}