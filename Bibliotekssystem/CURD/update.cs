using Bibliotekssystem.Models;
using System;
using Microsoft.EntityFrameworkCore;

public class Update
{
    public static void Run()
    {
        System.Console.WriteLine("Update Author, Book, loan\n");
        System.Console.WriteLine("1. Update author");
        System.Console.WriteLine("2. Update Book");
        System.Console.WriteLine("3. update Loan");

        var input = Console.ReadLine();

        switch(input)
        {
            case "1":
                UpdateAuth();
                break;
            case "2":
                UpdateBook();
                break;
            case "3":
                updateloan();
                break;
            default:
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid choice. Exiting update process.");
                Console.ForegroundColor = ConsoleColor.White;
                break;
        }
    }

    private static void updateloan()
    {
        using (var context = new AppDbContext())
        {
            var loans = context.Loans.ToList();
            if(!loans.Any())
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


            System.Console.WriteLine("Enter Loan ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out var loanId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid Loan ID.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            
            // Hämta lånet via loan ID och inkludera tillhörande bokdetaljer
            var loan = context.Loans.Include(L => L.Book).FirstOrDefault(l => l.Id == loanId);
            if(loan == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Not found");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            System.Console.WriteLine($"Current Borrower: {loan.BorrowerName}");
            System.Console.WriteLine("Enter new borrower (Enter to keep the current)");
            var borrow = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(borrow)) 
            {
                loan.BorrowerName = borrow;
            }

            System.Console.WriteLine($"Current Status: {(loan.IsReturned ? "Returned" : "Not Returned")}");
            System.Console.Write("Has the book been returned? (yes/no, enter to keep current): ");
            var returned = Console.ReadLine()?.ToLower();
            if (returned == "yes") loan.IsReturned = true;

            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("Updated successfully.");   
            Console.ForegroundColor = ConsoleColor.White;       
        }
    }

    private static void UpdateBook()
    {
        using (var context = new AppDbContext())
        {

            var books = context.Books.ToList();
            if(!books.Any())
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
            System.Console.Write("Enter Book ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid Book ID.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var book = context.Books.Find(bookId);
            if (book == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Book not found.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            System.Console.WriteLine($"Current Book title: {book.Title}");
            System.Console.WriteLine("Enter new title (leave blank to keep current): ");
            var title = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(title))
            {
                book.Title = title;
            }
            
            System.Console.WriteLine($"Current Summary\n {book.Summary}");
            System.Console.Write("Enter new summary (leave blank to keep current): ");
            var summary = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(summary)) 
            {
                book.Summary = summary;
            }

            System.Console.WriteLine($"Current Availability: {(book.IsAvailable ? "Available" : "Unavailable")}");
            System.Console.Write("Is the book available right now? (yes/no, leave blank to keep current): ");
            var availability = Console.ReadLine()?.ToLower();
            if (availability == "yes") 
            {
                book.IsAvailable = true;
            }
            else if (availability == "no") 
            {
                book.IsAvailable = false;
            }
            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Book updated successfully.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    private static void UpdateAuth()
    {
        using (var context = new AppDbContext())
        {
            var authors = context.Authors.ToList();

            if(!authors.Any())
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
            
            System.Console.WriteLine("Enter Author Id: ");
            if(!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Invalid Author Id: ");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var author = context.Authors.Find(authorId); //vi hittar author med id
            if(author == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("Author not found");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            System.Console.WriteLine($"Current Author {author.FirstName} {author.LastName}");
            System.Console.WriteLine("Enter new first name (press enter to keep the current name)");
            var firstname = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(firstname)) 
            {
               author.FirstName = firstname;
            }

            System.Console.WriteLine("Enter new last name (press enter to keep the current name)");
            var lastname = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(lastname))
            {
                author.LastName = lastname;
            }

            System.Console.WriteLine($"\nDate of birth: {author.BirthYear:yyyy-MM-dd}");
            System.Console.WriteLine("Enter new date of birth (press enter to keep the current name)");
            var dateofbirth = Console.ReadLine();
            if(!string.IsNullOrWhiteSpace(dateofbirth))
            {
                if (DateTime.TryParse(dateofbirth, out DateTime birthDate))
                {
                    author.BirthYear = birthDate;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    System.Console.WriteLine("Invalid date format");
                    Console.ForegroundColor = ConsoleColor.White;
                }
            }            

            Console.WriteLine($"Current Biography:\n {author.Biography}");
            Console.Write("Enter new biography (press enter to keep the current name): ");
            var biography = Console.ReadLine();
            if (!string.IsNullOrWhiteSpace(biography)) 
            {
                author.Biography = biography;
            }

            context.SaveChanges();
            Console.ForegroundColor = ConsoleColor.Green;
            System.Console.WriteLine("athor updated successfully");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }

    
}