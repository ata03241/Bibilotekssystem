using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;
using System;

public class loanhistory
{
    public static void Run()
    {
        using(var context = new AppDbContext())
        {
            var historys = context.Loans.Include(l => l.Book)
                .ThenInclude(l => l.BookAuthors)
                //select for att ta data från specifik
                .Select(l => new
                {
                    l.Book.Title,
                    l.Book.ReleaseDate,
                    l.BorrowerName,
                    l.LoanDate,
                    l.ReturnDate,
                    l.IsReturned
                })
                .ToList();
            
            if(!historys.Any())
            {
                Console.ForegroundColor = ConsoleColor.Red;
                System.Console.WriteLine("There is no loan history");
                Console.ForegroundColor = ConsoleColor.White;
            }
            else
            {
                foreach (var item in historys)
                {
                    System.Console.WriteLine($"Book Title: {item.Title}");
                    System.Console.WriteLine($"Book Releasedate: {item.ReleaseDate}");
                    System.Console.WriteLine($"Borrower name: {item.BorrowerName}");
                    System.Console.WriteLine($"Loan date: {item.LoanDate}");
                    System.Console.WriteLine($"Return date: {item.ReturnDate}");
                    System.Console.WriteLine($"Is Returned: {(item.IsReturned ? "Yes" : "No")}\n");
                }
            }
        }
    }
}