using System;
using System.Security.Cryptography;
using Bibliotekssystem.Models;
using Microsoft.EntityFrameworkCore;


class Program
{
    static void Main(string[] args)
    {
        
        System.Console.WriteLine("Welcome to library");
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
                AddAuthor();
                break;
            case "2":
                AddBook();
                break;
            case "3":
                AddBookAuthorRelationship();
                break;
            case "4":
                AddLoan();
                break;
            case "5":
                ReturnBook();
                break;
            case "6":
                // Removeabl(); //kvar att fixa
                break;
            case "7":
                ListBookauth();
                break;
            case "8":
                ListLoanBook();
                break;

        }
    }

    private static void ListLoanBook()
    {
        using(var context = new AppDbContext())
        {
            var loans = context.Loans.Include(l => l.Book)
                //vi ska lista bara loan book
                .Where(l => l.IsReturned ==false)
                .ToList();
            
            if(!loans.Any()) //om de finns ingen 
            {
                System.Console.WriteLine("There are no loan books");
            }
            else
            {
                foreach (var loan in loans)
                {
                    System.Console.WriteLine($"\nBook: {loan.Book.Title}");
                    System.Console.WriteLine($"borrower: {loan.BorrowerName}\n");
                }

            }
            
        }
    }

    private static void ListBookauth()
    {
        using(var context = new AppDbContext())
        {
            var books = context.Books.Include(b => b.BookAuthors)
                .ThenInclude(BookAuthor => BookAuthor.Author)
                .ToList();
            
            foreach (var book in books)
            {
                System.Console.WriteLine($"\nBook: {book.Title}");
                foreach (var author in book.BookAuthors)
                {
                    System.Console.WriteLine($"Author: {author.Author.FirstName} {author.Author.LastName}");
                    System.Console.WriteLine($"Bio: {author.Author.Biography}\n");
                }
            }
        }
    }

    private static void ReturnBook()
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
               book.IsAvailable = true; //true if when returing
            }
            context.SaveChanges();

            Console.WriteLine($"Book with Loan ID {loanId} has been returned.");
        }
    }


    private static void AddLoan()
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
            if(book == null)
            {
                Console.WriteLine("Book not found.");
                 return;
            }

            if(!book.IsAvailable)
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


    private static void AddBookAuthorRelationship()
    {
        using (var context = new AppDbContext())
        {
            Console.Write("Enter Book ID: ");
            if (!int.TryParse(Console.ReadLine(), out var bookId))
            {
                Console.WriteLine("Invalid Book ID.");
                return;
            }

            Console.Write("Enter Author ID: ");
            if (!int.TryParse(Console.ReadLine(), out var authorId))
            {
                Console.WriteLine("Invalid Author ID.");
                return;
            }

            var bookAuthor = new BookAuthor
            {
                BookID = bookId,
                AuthorId = authorId
            };

            context.BookAuthors.Add(bookAuthor);
            context.SaveChanges();
            Console.WriteLine($"Relationship added between Book {bookId} and Author {authorId}.");
        }
    }


    private static void AddBook()
    {
        using (var context = new AppDbContext())
        {
            Console.WriteLine("Enter Book name: ");
            var bookName = Console.ReadLine();

            Console.WriteLine("Enter Book ReleaseDate (yyyy-MM-dd): ");
            var bookReleaseDate = Console.ReadLine();
            if (!DateTime.TryParse(bookReleaseDate, out var releaseDate))
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
                return;
            }

            Console.WriteLine("Enter Book Summary: ");
            var bookSummary = Console.ReadLine();

            Console.WriteLine("Enter Book Publisher: ");
            var publisher = Console.ReadLine();

            Console.WriteLine("Is the book available (yes/no): ");
            var availabilityInput = Console.ReadLine()?.ToLower();
            bool isAvailable = availabilityInput == "yes";


            var book = new Book
            {
                Title = bookName,
                ReleaseDate = releaseDate,
                Summary = bookSummary,
                Publisher = publisher,
                IsAvailable = isAvailable
            };

            context.Books.Add(book);
            context.SaveChanges();

            Console.WriteLine($"Book '{bookName}' added successfully!");
        }

    }

    private static void AddAuthor()
    {
        using (var context = new AppDbContext())
        {
            System.Console.Write("Write Author First Name: ");
            var authorName = Console.ReadLine();
            System.Console.WriteLine("Write Author Last Name: ");
            var authorLast = Console.ReadLine();
            Console.Write("Author's Date of Birth (yyyy-MM-dd): ");
            var birthDateInput = Console.ReadLine();

            if (!DateTime.TryParse(birthDateInput, out DateTime birthDate))
            {
                Console.WriteLine("Invalid date format. Please use yyyy-MM-dd.");
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
            Console.WriteLine($"Author {authorName} {authorLast} added successfully!");
        }
    }
}

