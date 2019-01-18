using MonashDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonashDemo.Infrastructure
{
    public sealed class LibraryContext : IDisposable
    {
        private static LibraryContext instance = null;
        private static readonly object _lock = new object();
        
        public ICollection<Book> Books { get; set; }
        public ICollection<Borrower> Borrowers { get; set; }
        public ICollection<BookBorrower> BooksBorrowed { get; set; }

        public LibraryContext()
        {
            Books = new List<Book>();
            Books.Add(new Book() { BookId = 1, Title = "Domain Driven Design", Author = "Eric J.Evans", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 2, Title = "Patterns of Enterprise Application Architecture", Author = "Martin Fowler", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 3, Title = "C# In Depth", Author = "Jon Skeet", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 4, Title = "Pro ASP.NET MVC 5", Author = "Adam Freeman", IsBorrowed = Convert.ToBoolean(1) });
            Books.Add(new Book() { BookId = 5, Title = "Beginning HTML5 and CSS3 For Dummies", Author = "Ed Tittel", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 6, Title = "Professional JQuery", Author = "Csar Otero", IsBorrowed = Convert.ToBoolean(1) });

            Borrowers = new List<Borrower>();
            Borrowers.Add(new Borrower() { BorrowerId = 1, FirstName = "Ram", LastName = "Pusarla" });
            Borrowers.Add(new Borrower() { BorrowerId = 2, FirstName = "Yuvin", LastName = "Pusarla" });

            BooksBorrowed = new List<BookBorrower>();
            BooksBorrowed.Add(new BookBorrower() { Id = 1, BookId = 4, BorrowerId = 2, DateBorrowed = DateTime.Today.AddDays(-10) });
            BooksBorrowed.Add(new BookBorrower() { Id = 2, BookId = 6, BorrowerId = 2, DateBorrowed = DateTime.Today.AddDays(-15) });

        }
        public static LibraryContext Instance
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new LibraryContext();
                    }
                    return instance;
                }
            }
        }
        public LibraryContext GlobalLibraryContext
        {
            get
            {
                lock (_lock)
                {
                    return (LibraryContext)HttpContext.Current.Session["libraryContext"] == null ? instance : (LibraryContext)HttpContext.Current.Session["libraryContext"];
                }
            }
            set
            {
                lock (_lock)
                {                    
                    HttpContext.Current.Session["libraryContext"] = value;
                    instance = value;
                }
            }
        }

        public void Dispose()
        {
            instance.Dispose();
        }
    }
}
