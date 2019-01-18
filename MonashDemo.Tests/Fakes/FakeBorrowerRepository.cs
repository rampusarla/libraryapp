using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonashDemo.Core;
using MonashDemo.Tests.Fakes;
using MonashDemo.Core.Models;
using System.Web.Mvc;
using MonashDemo.Core.Interfaces;
using MonashDemo.Core.ViewModels;

namespace MonashDemo.Tests.Fakes
{
    public class FakeBorrowerRepository : IBorrowerRepository
    {
        public FakeLibraryContext context;
        public FakeBorrowerRepository()
        {
            context = new FakeLibraryContext();
            context.InitializeData();
        }
        public void AddBorrower(Borrower borrower)
        {
            context.Borrowers.Add(borrower);
        }

        public void AssignBookToBorrower(AddBookBorrowerViewModel addBookBorrowerViewModel)
        {
            var bookBorrower = new BookBorrower();
            bookBorrower.Id = context.BooksBorrowed.Count == 0 ? 1 : context.BooksBorrowed.Count + 1;
            bookBorrower.BookId = addBookBorrowerViewModel.BookId;
            bookBorrower.BorrowerId = addBookBorrowerViewModel.BorrowerId;
            bookBorrower.DateBorrowed = System.DateTime.Now;

            context.BooksBorrowed.Add(bookBorrower);
            context.Books.Where(b => b.BookId == bookBorrower.BookId).FirstOrDefault().IsBorrowed = true;
        }

        public DisplayBookBorrowerViewModel GetAllBooksBorrowers()
        {
            var bookBorrowerViewModel = new DisplayBookBorrowerViewModel();
            bookBorrowerViewModel.Books = context.Books.
                                Where(b => b.IsBorrowed == false).
                                Select(c => new SelectListItem
                                {
                                    Value = c.BookId.ToString(),
                                    Text = c.Title
                                });
            bookBorrowerViewModel.Borrowers = context.Borrowers.
                                Select(c => new SelectListItem
                                {
                                    Value = c.BorrowerId.ToString(),
                                    Text = c.FirstName + " " + c.LastName
                                });
            return bookBorrowerViewModel;
        }

        public IEnumerable<Borrower> GetAllBorrowers(string sortOrder)
        {
            var borrowers = context.Borrowers.ToList();
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

        public bool IsBookBorrowed(int bookId)
        {
            var book = context.Books.Where(b => b.BookId == bookId);
            if (book.Count() >= 1)
                return (book.FirstOrDefault().IsBorrowed == false);
            else return false;
        }
        public bool IsBorrowerExists(int borrowerId)
        {
            return (context.Borrowers.Where(b => b.BorrowerId == borrowerId).Count() == 1);
        }
        public bool IsBorrowerExistsWithSameName(Borrower borrower)
        {
            var firstName = borrower.FirstName == null ? null : borrower.FirstName.Trim().ToUpper();
            var lastName = borrower.LastName == null ? null : borrower.LastName.Trim().ToUpper();

            if (firstName == null || lastName == null)
                return true;

            return (context.Borrowers.Where(b => (b.FirstName.Trim().ToUpper() == firstName && b.LastName.Trim().ToUpper() == lastName)).FirstOrDefault() != null);                        
        }
    }
}
