using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using MonashDemo.Infrastructure;
using MonashDemo.Core.Interfaces;

namespace MonashDemo.Core.Repositories
{
    public class BorrowerRepository : IDisposable, IBorrowerRepository
    {
        private LibraryContext libraryContext;
        
        public BorrowerRepository() : 
            this(LibraryContext.Instance)
        {
            
        }
        public BorrowerRepository(LibraryContext context)
        {
            libraryContext = context.GlobalLibraryContext;            
        }

        // This functions adds a new Borrower to the existing Borrower Collection.
        public void AddBorrower(Borrower borrower)
        {
            borrower.BorrowerId = libraryContext.Borrowers.Count == 0 ? 1 : libraryContext.Borrowers.Count + 1;
            libraryContext.Borrowers.Add(borrower);            
            libraryContext.GlobalLibraryContext = libraryContext;
        }

        // This function returns the list of all Borrowers.
        public IEnumerable<Borrower> GetAllBorrowers(string sortOrder)
        {
            var borrowers = libraryContext.Borrowers.ToList();
            switch (sortOrder)
            {                
                case "firstname_desc":
                    borrowers = borrowers.OrderByDescending(s => s.FirstName).ToList();
                    break;
                case "lastname":
                    borrowers = borrowers.OrderBy(s => s.LastName).ToList();                                
                    break;
                case "lastname_desc":
                    borrowers = borrowers.OrderByDescending(s => s.LastName).ToList();
                    break;
                default:
                    borrowers = borrowers.OrderBy(s => s.FirstName).ToList();
                    break;
            }
            return borrowers;
        }

        // This function returns the list of all available Books and Borrowers.
        public DisplayBookBorrowerViewModel GetAllBooksBorrowers()
        {
            var bookBorrowerViewModel = new DisplayBookBorrowerViewModel();
            bookBorrowerViewModel.Books = libraryContext.Books.
                                Where(b => b.IsBorrowed == false).
                                Select(c => new SelectListItem
                                {
                                    Value = c.BookId.ToString(),
                                    Text = c.Title
                                });
            bookBorrowerViewModel.Borrowers = libraryContext.Borrowers.
                                Select(c => new SelectListItem
                                {
                                    Value = c.BorrowerId.ToString(),
                                    Text = c.FirstName + " " + c.LastName
                                });
            return bookBorrowerViewModel;
        }

        // This function assigns a Book to a Borrower.
        public void AssignBookToBorrower(AddBookBorrowerViewModel addBookBorrowerViewModel)
        {
            var bookBorrower = new BookBorrower();
            bookBorrower.Id = libraryContext.BooksBorrowed.Count == 0 ? 1 : libraryContext.BooksBorrowed.Count + 1;
            bookBorrower.BookId = addBookBorrowerViewModel.BookId;
            bookBorrower.BorrowerId = addBookBorrowerViewModel.BorrowerId;
            bookBorrower.DateBorrowed = System.DateTime.Now;            

            libraryContext.BooksBorrowed.Add(bookBorrower);
            libraryContext.Books.Where(b => b.BookId == bookBorrower.BookId).FirstOrDefault().IsBorrowed = true;
            
            libraryContext.GlobalLibraryContext = libraryContext;

        }

        // This function checks if the book has already been borrowed.
        public bool IsBookBorrowed(int bookId)
        {
            var book = libraryContext.Books.Where(b => b.BookId == bookId);
            if (book.Count() >= 1)
                return (book.FirstOrDefault().IsBorrowed == false);
            else return false;
        }

        // This function checks if the Borrower already exists with same Borrower Id.
        public bool IsBorrowerExists(int borrowerId)
        {
            return (libraryContext.Borrowers.Where(b => b.BorrowerId == borrowerId).Count() == 1);
        }

        public bool IsBorrowerExistsWithSameName(Borrower borrower)
        {
            var firstName = borrower.FirstName == null ? null : borrower.FirstName.Trim().ToUpper();
            var lastName = borrower.LastName == null ? null : borrower.LastName.Trim().ToUpper();

            if (firstName == null || lastName == null)
                return true;

            return (libraryContext.Borrowers.Where(b => (b.FirstName.Trim().ToUpper() == firstName && b.LastName.Trim().ToUpper() == lastName)).FirstOrDefault() != null);                        
        }

        public void Dispose()
        {
            libraryContext.Dispose();
        }

    }
}