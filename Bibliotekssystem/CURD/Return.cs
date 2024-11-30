using Bibliotekssystem.Models;
using System;

public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Write("Enter Loan ID to return the book: ");
            if (!int.TryParse(Console.ReadLine(), out var loanId))
            {
                Console.WriteLine("Invalid Loan ID.");
                return;
            }

            var loan = context.Loans.Find(loanId);

            if (loan == null)
            {
                Console.WriteLine("Loan not found.");
                return;
            }

            if (loan.IsReturned)
            {
                Console.WriteLine("This book has already been returned.");
                return;
            }
            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;

            var book = context.Books.Find(loan.BookId);
            if(book != null)
            {
               book.IsAvailable = true; //true when returing
            }
            context.SaveChanges();

            Console.WriteLine($"Book with Loan ID {loanId} has been returned.");
        }
    }
}