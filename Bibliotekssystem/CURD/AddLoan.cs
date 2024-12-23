using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class Addloans
{
    public static void Run()
    {
        using (var context = new AppDbContext())
        {

            const int LoanLimt = 3;

            System.Console.WriteLine("\n Available Books\n");
            var books = context.Books.Include(bo => bo.BookAuthors)
                .ThenInclude(ba => ba.Author)
                .ToList();

            if (!books.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("No books available right now.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            foreach (var boo in books)
            {
                Console.WriteLine($"Book ID: {boo.Id}, Title: {boo.Title}, Publisher: {boo.Publisher ?? "Unknown"}, Available: {(boo.IsAvailable ? "Yes" : "No")}\n");
            }


            Console.Write("Enter Borrower's Name: ");
            var borrower = Console.ReadLine();

            var loancount = context.Loans
                .Count(l => l.BorrowerName == borrower && !l.IsReturned);

            if (loancount >= LoanLimt)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Borrower '{borrower}' has reached the loan limit of {LoanLimt} active loans.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Invalid Book ID.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            var book = context.Books.Find(bookId);
            if (book == null)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Book not found.");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }

            if (!book.IsAvailable)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("The book is currently unavialble");
                Console.ForegroundColor = ConsoleColor.White;
                return;
            }
            var loan = new Loan
            {
                BookId = bookId,
                BorrowerName = borrower,
                LoanDate = DateTime.Now,
                IsReturned = false
            };

            context.Loans.Add(loan);
            context.SaveChanges();
            
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine($"Loan added for Book {bookId}, borrowed by {borrower}.");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}