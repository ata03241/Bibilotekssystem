using Bibliotekssystem.Models;
using System;

public class Addloans
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return;
            }

            var book = context.Books.Find(bookId);
            if (book == null)
            {
                Console.WriteLine("Book not found.");
                return;
            }

            if (!book.IsAvailable)
            {
                System.Console.WriteLine("The book is currently unavialble");
                return;
            }

            Console.Write("Enter Borrower's Name: ");
            var borrower = Console.ReadLine();
            var loan = new Loan
            {
                BookId = bookId,
                BorrowerName = borrower,
                LoanDate = DateTime.Now,
                IsReturned = false
            };

            context.Loans.Add(loan);
            context.SaveChanges();

            Console.WriteLine($"Loan added for Book {bookId}, borrowed by {borrower}.");
        }
    }
}