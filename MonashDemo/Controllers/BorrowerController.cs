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
using MonashDemo.Core.ServiceLayer;
using MonashDemo.Core.Repositories;

namespace MonashDemo.Controllers
{    
    public class BorrowerController : Controller
    {        
        private IBorrowerService borrowerService;

        public BorrowerController()            
        {
            borrowerService = new BorrowerService(new ModelStateWrapper(this.ModelState), new BorrowerRepository());
        }
        public BorrowerController(IBorrowerService _borrowerService)
        {            
            borrowerService = _borrowerService;
        }

        //This function lists all the borrowers.
        //GET: /Borrower/
        [HttpGet]
        //public ActionResult Index(string sortOrder, string currentFilter, int? page)
        public ActionResult Index(string sortOrder, int? page)
        {
            ViewBag.CurrentSort = sortOrder;
            ViewBag.FirstNameSortParm = String.IsNullOrEmpty(sortOrder) ? "firstname_desc" : "";
            ViewBag.LastNameSortParm = sortOrder == "lastname" ? "lastname_desc" : "lastname";

            var borrowers = borrowerService.GetAllBorrowers(sortOrder);

            if (borrowers.Count() > 0)
                return View(borrowers.ToPagedList(pageNumber: page ?? 1, pageSize: 5));
            else
            {
                var error = InitializeErrors(ErrorMessages.EmptyBorrowersHeading, ErrorMessages.EmptyBorrowersMessage);
                return View("Empty", error);
            }
        }        

        
        // GET: /Borrower/Create
        [HttpGet]
        public ActionResult Create()
        {
            return View();
        }

        // This function creates a new Borrower.
        // POST: /Borrowers/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Borrower borrower)
        {            
            if(borrowerService.CreateBorrower(borrower))                
            {
                return RedirectToAction("Index");             
            }
            else return View(borrower);
        }

        // This function displays Borrowers and the available Books.
        // GET: /Borrower/BookBorrow
        [HttpGet]
        public ActionResult BookBorrow()
        {
            var booksBorrowers = borrowerService.GetAllBooksBorrowers();
            
            if(borrowerService.IsAtleastOneBookAndOneBorrowerExist(booksBorrowers))            
                return View(booksBorrowers);
            else if(!borrowerService.IsAtleastOneBookExist(booksBorrowers.Books))
            {                
                var error = InitializeErrors(ErrorMessages.EmptyBooksHeading, ErrorMessages.EmptyBooksMessage);
                return View("Empty", error);
            }
            else
            {
                var error = InitializeErrors(ErrorMessages.EmptyBorrowersHeading, ErrorMessages.EmptyBorrowersMessage);                
                return View("Empty", error);
            }
        }

        // This function assigns a Book to Borrower.
        // POST: /Borrower/BookBorrow/
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult BookBorrow(AddBookBorrowerViewModel addBookBorrowerViewModel)
        {
            if(borrowerService.AssignBorrowerToBook(addBookBorrowerViewModel))
            {                
                return RedirectToAction("Index","Book");
            }
            else return View(addBookBorrowerViewModel);            
        }

        [NonAction]
        private ErrorViewModel InitializeErrors(string heading, string message)
        {
            var error = new ErrorViewModel()
            {
                ErrorHeading = heading,
                ErrorMessage = message
            };

            return error;
        }

    }
}
