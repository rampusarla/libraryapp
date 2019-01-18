using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace MonashDemo.Core.Interfaces
{
    public interface IBorrowerService
    {
        bool AssignBorrowerToBook(AddBookBorrowerViewModel addBookBorrowerViewModel);
        bool CreateBorrower(Borrower borrower);
        IEnumerable<Borrower> GetAllBorrowers(string sortOrder);        
        DisplayBookBorrowerViewModel GetAllBooksBorrowers();
        bool IsAtleastOneBorrowerExist(IEnumerable<SelectListItem> borrowers);
        bool IsAtleastOneBookExist(IEnumerable<SelectListItem> books);
        bool IsAtleastOneBookAndOneBorrowerExist(DisplayBookBorrowerViewModel bbVM);
        
    }
}
