using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MonashDemo.Core;
using MonashDemo.Tests.Fakes;
using System.Web.Mvc;
using MonashDemo.Core.Interfaces;
using MonashDemo.Core.ViewModels;
using MonashDemo.Core.Models;


namespace MonashDemo.Tests.Fakes
{
    public class FakeBookRepository : IBookRepository
    {
        public FakeLibraryContext context;
        public FakeBookRepository()
        {
            context = new FakeLibraryContext();
            context.InitializeData();
        }
        public void AddBook(BookViewModel bookVM)
        {
            var book = new Book();
            book.BookId = context.Books.Count == 0 ? 1 : context.Books.Count + 1;
            book.Author = bookVM.Author;
            book.Title = bookVM.Title;
            book.IsBorrowed = false;

            context.Books.Add(book);
        }

        public IEnumerable<Book> GetAllBooks(string sortOrder, string searchString)
        {
            var books = context.Books.ToList();            
            if (!String.IsNullOrEmpty(searchString))
            {
                books = books.Where(b => b.Author.ToUpper().Contains(searchString.ToUpper())
                                       || b.Title.ToUpper().Contains(searchString.ToUpper())).ToList();
            }

            switch (sortOrder)
            {
                case "title_desc":
                    books = books.OrderByDescending(s => s.Title).ToList();
                    break;
                case "author":
                    books = books.OrderBy(s => s.Author).ToList();
                    break;
                case "author_desc":
                    books = books.OrderByDescending(s => s.Author).ToList();
                    break;
                default:
                    books = books.OrderBy(s => s.Title).ToList();
                    break;
            }
            return books;
        }

        public IEnumerable<Book> GetAllOverdueBooks()
        {
            var books = context.BooksBorrowed
                                      .Where(b => b.DateBorrowed < System.DateTime.Today.AddDays(-7));
            var count = books.Count();
            var overdueBooks = books.Select(b =>
            {
                var book = context.Books.Where(a => a.BookId == b.BookId).FirstOrDefault();
                if (book == null)
                    return new Book();
                else return new Book
                { 
                    BookId = book.BookId,
                    Author = book.Author,
                    Title = book.Title,
                    DateBorrowed = b.DateBorrowed,
                    IsBorrowed = book.IsBorrowed                   
                };
            }).ToList();

            return overdueBooks;
        }
        public bool IsBookAlreadyExists(BookViewModel bookVM)
        {
            var title = bookVM.Title == null ? null : bookVM.Title.Trim().ToUpper();
            var author = bookVM.Author == null ? null : bookVM.Author.Trim().ToUpper();

            if (title == null || author == null)
                return true;
            else return (context.Books.Where(b => (b.Author.Trim().ToUpper() == author && b.Title.Trim().ToUpper() == title)).FirstOrDefault() != null);
        }
    }
}
