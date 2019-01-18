using MonashDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonashDemo.Tests.Fakes
{
    public class FakeLibraryContext
    {        
        public ICollection<Book> Books { get; set; }
        public ICollection<Borrower> Borrowers { get; set; }
        public ICollection<BookBorrower> BooksBorrowed { get; set; }

        public void InitializeData()
        {
            Books = new List<Book>();
            Books.Add(new Book() { BookId = 1, Author = "Yashwant Kanitkar", Title = "C Language Complete Reference", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 2, Author = "Martin Fowler", Title = "Patterns of Enterprise Application Architecture", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 3, Author = "Author3", Title = "Title3", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 4, Author = "Author4", Title = "Title4", IsBorrowed = Convert.ToBoolean(1) });
            Books.Add(new Book() { BookId = 5, Author = "Author5", Title = "Title5", IsBorrowed = Convert.ToBoolean(0) });
            Books.Add(new Book() { BookId = 6, Author = "Author6", Title = "Title6", IsBorrowed = Convert.ToBoolean(1) });

            Borrowers = new List<Borrower>();
            Borrowers.Add(new Borrower() { BorrowerId = 1, FirstName = "Ram", LastName = "Pusarla" });
            Borrowers.Add(new Borrower() { BorrowerId = 2, FirstName = "Yuvin", LastName = "Pusarla" });

            BooksBorrowed = new List<BookBorrower>();
            BooksBorrowed.Add(new BookBorrower() { Id = 1, BookId = 4, BorrowerId = 2, DateBorrowed = DateTime.Today.AddDays(-10) });
            BooksBorrowed.Add(new BookBorrower() { Id = 2, BookId = 6, BorrowerId = 2, DateBorrowed = DateTime.Today.AddDays(-15) });

        }        
    }
}
