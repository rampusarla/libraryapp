using System;
using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using System.Collections.Generic;


namespace MonashDemo.Core.Interfaces
{
    public interface IBorrowerRepository
    {
        void AddBorrower(Borrower borrower);
        void AssignBookToBorrower(AddBookBorrowerViewModel addBookBorrowerViewModel);
        DisplayBookBorrowerViewModel GetAllBooksBorrowers();
        IEnumerable<Borrower> GetAllBorrowers(string sortOrder);
        bool IsBookBorrowed(int bookId);
        bool IsBorrowerExists(int borrowerId);
        bool IsBorrowerExistsWithSameName(Borrower borrower);
    }
}
