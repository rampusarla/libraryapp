using MonashDemo.Core.Interfaces;
using MonashDemo.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using MonashDemo.Core.ViewModels;
using MonashDemo.Core.Errors;
using MonashDemo.Core.Repositories;
using MonashDemo.Core.ServiceLayer;

namespace MonashDemo.Controllers
{    
    public class BookController : Controller
    {        
        private IBookService bookService;

        public BookController()            
        {
            bookService = new BookService(new ModelStateWrapper(this.ModelState), new BookRepository());
        }
        public BookController(IBookService _bookService)
        {            
            bookService = _bookService;
        }

        // This function lists all the books
        // GET: /Book/
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.TitleSortParam = String.IsNullOrEmpty(sortOrder) ? "title_desc" : "";
            ViewBag.AuthorSortParm = sortOrder == "author" ? "author_desc" : "author";
            ViewBag.CurrentFilter = searchString ?? currentFilter;

            var books = bookService.GetAllBooks(sortOrder, searchString);                        
            
            if (books.Count() > 0)
                return View(books.ToPagedList(pageNumber: page ?? 1, pageSize: 5));
            else if (searchString != null)
            {
                var error = InitializeErrors(ErrorMessages.BookNotFoundHeading, ErrorMessages.BookNotFoundMessage);                
                return View("Empty", error);
            }
            else
            {
                var error = InitializeErrors(ErrorMessages.EmptyBooksHeading, ErrorMessages.EmptyBooksMessage);
                return View("Empty", error);
            }
        }

        // This function lists all books that have been overdue more than 1 week.
        // GET: /Book/ListOverdueBooks/
        [HttpGet]
        public ActionResult ListOverdueBooks()
        {
            var overdueBooks = bookService.GetAllOverdueBooks();
            if (bookService.IsAtleastOneOverdueBookExist())
                return View(overdueBooks);
            else
            {
                var error = InitializeErrors(ErrorMessages.EmptyOverdueBooksHeading, ErrorMessages.EmptyOverdueBooksMessage);
                return View("Empty", error);
            }
        }
        
        // Displays the Book Create view
        // GET: /Book/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }
        
        // This function is to create a new book
        // POST: /Book/Create
        [HttpPost]
        [ValidateAntiForgeryToken]        
        public ActionResult Create(BookViewModel bookVM)
        {            
            if (bookService.CreateBook(bookVM))
            {                
                return RedirectToAction("Index");                
            }
            else return View(bookVM);
        }

        // Function used to initialize Errors
        [NonAction]
        private ErrorViewModel InitializeErrors(string heading, string message)
        {
            var error = new ErrorViewModel() { 
                 ErrorHeading = heading,
                 ErrorMessage = message
            };

            return error;
        }
    }
}
