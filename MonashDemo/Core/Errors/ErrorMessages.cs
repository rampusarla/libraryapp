using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonashDemo.Core.Errors
{
    public static class ErrorMessages
    {
        public const string EmptyBorrowersHeading = "Empty Borrowers";
        public const string EmptyBorrowersMessage = "Sorry - No Borrowers are available";
        public const string EmptyBooksHeading = "Empty Books";
        public const string EmptyBooksMessage = "Sorry - No Books are available";
        public const string BookNotFoundHeading = "Book Not Found";
        public const string BookNotFoundMessage = "Sorry - No books are available with the text entered";
        public const string BorrowerNotFoundHeading = "Borrower Not Found";
        public const string BorrowerNotFoundMessage = "Sorry - No Borrowers are available with the text entered";
        public const string EmptyOverdueBooksHeading = "No Overdue Books available";
        public const string EmptyOverdueBooksMessage = "Sorry - No books have been overdue more than 1 Week";        
        public const string BorrowerAlreadyExistsHeading = "Borrower Already Exists";        
        public const string BorrowerAlreadyExistsMessage = "Borrower already exists. Please choose a different Borrower";
        public const string BookAlreadyExistsHeading = "Book Already Exists";
        public const string BookAlreadyExistsMessage = "Book already exists. Please enter a different Book Title";
    }
}