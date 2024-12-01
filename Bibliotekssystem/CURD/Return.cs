using Bibliotekssystem.Models;
using System;
using System.Linq;

public class ReturnBook
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {
            Console.Write("Enter Borrower's Name: ");
            var borrowerName = Console.ReadLine();

            Console.Write("Enter Book ID to return: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return;
            }
            
            var loan = context.Loans
                .FirstOrDefault(l => l.BorrowerName == borrowerName && l.BookId == bookId && !l.IsReturned);

            if (loan == null)
            {
                Console.WriteLine("No active loan found for this borrower and book.");
                return;
            }

            
            loan.IsReturned = true;
            loan.ReturnDate = DateTime.Now;

            
            var book = context.Books.Find(bookId);
            if (book != null)
            {
                book.IsAvailable = true; 
            }

            context.SaveChanges();

            Console.WriteLine($"Book with ID {bookId} has been returned by {borrowerName}.");
        }
    }
}
