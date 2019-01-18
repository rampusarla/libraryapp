using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using MonashDemo.Core.Interfaces;
using MonashDemo.Infrastructure;

namespace MonashDemo.Core.Repositories
{
    public class BookRepository : IDisposable, IBookRepository
    {
        private LibraryContext libraryContext;

        public BookRepository() : 
            this(LibraryContext.Instance)
        {
            
        }
        public BookRepository(LibraryContext context)
        {
            libraryContext = context.GlobalLibraryContext;
        }

        // This function adds a new book to the existing Book collection.
        public void AddBook(BookViewModel bookVM)
        {
            var book = new Book();
            book.BookId = libraryContext.Books.Count == 0 ? 1 : libraryContext.Books.Count + 1;
            book.Author = bookVM.Author;
            book.Title = bookVM.Title;
            book.IsBorrowed = false;            

            libraryContext.Books.Add(book);
                       
            libraryContext.GlobalLibraryContext = libraryContext;
        }

        // This function return the list of all Books overdue by more than 1 Week.
        public IEnumerable<Book> GetAllOverdueBooks()
        {            
            var books = libraryContext.BooksBorrowed
                                      .Where(b => b.DateBorrowed < System.DateTime.Today.AddDays(-7));                                                                                        
            var overdueBooks =  books.Select(b => 
                                  {
                                      var book = libraryContext.Books.Where(a => a.BookId == b.BookId).FirstOrDefault();
                                      if (book == null)
                                          return new Book();
                                      else return new Book
                                      {
                                        BookId = book.BookId,
                                        Author = book.Author,
                                        Title = book.Title,
                                        DateBorrowed = b.DateBorrowed,
                                        IsBorrowed = book.IsBorrowed,                                        
                                      };                                           
                                  }).ToList();                        
            return overdueBooks;
                                
        }

        // This function returns the list of all Books.
        public IEnumerable<Book> GetAllBooks(string sortOrder, string searchString)
        {
            var books = libraryContext.Books.ToList();
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

        //This function checks if the Book already exists.
        public bool IsBookAlreadyExists(BookViewModel bookVM)
        {
            var title = bookVM.Title == null ? null : bookVM.Title.Trim().ToUpper();
            var author = bookVM.Author == null ? null : bookVM.Author.Trim().ToUpper();

            if (title == null || author == null)
                return true;

            return (libraryContext.Books.Where(b => (b.Title.Trim().ToUpper() == title)).FirstOrDefault() != null);
        }

        public void Dispose()
        {
            libraryContext.Dispose();            
        }


    }
}