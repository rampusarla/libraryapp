using MonashDemo.Core.Errors;
using MonashDemo.Core.Interfaces;
using MonashDemo.Core.Models;
using MonashDemo.Core.Repositories;
using MonashDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonashDemo.Core.ServiceLayer
{
    public class BorrowerService : IBorrowerService
    {
        private IValidationDictionary _validatonDictionary;
        private IBorrowerRepository _repository;        

        public BorrowerService(IValidationDictionary validationDictionary, IBorrowerRepository repository)
        {
            _validatonDictionary = validationDictionary;
            _repository = repository;
        }

        public bool AssignBorrowerToBook(AddBookBorrowerViewModel addBookBorrowerViewModel)
        {
            if (!_repository.IsBookBorrowed(addBookBorrowerViewModel.BookId))
                _validatonDictionary.AddError(ErrorMessages.BookNotFoundHeading, ErrorMessages.BookNotFoundMessage);
            else if (!_repository.IsBorrowerExists(addBookBorrowerViewModel.BorrowerId))
                _validatonDictionary.AddError(ErrorMessages.BorrowerNotFoundHeading, ErrorMessages.BorrowerNotFoundMessage);

            if (_validatonDictionary.IsValid)
            {
                _repository.AssignBookToBorrower(addBookBorrowerViewModel);
                return true;
            }
            else return false;
        }
        public bool CreateBorrower(Borrower borrower)
        {
            if (_repository.IsBorrowerExistsWithSameName(borrower))
                _validatonDictionary.AddError(ErrorMessages.BorrowerAlreadyExistsHeading, ErrorMessages.BorrowerAlreadyExistsMessage);

            if (_validatonDictionary.IsValid)
            {
                _repository.AddBorrower(borrower);
                return true;
            }
            else return false;
        }

        public IEnumerable<Borrower> GetAllBorrowers(string sortOrder)
        {
            return _repository.GetAllBorrowers(sortOrder).ToList();
        }

        public DisplayBookBorrowerViewModel GetAllBooksBorrowers()
        {
            return _repository.GetAllBooksBorrowers();
        }

        public bool IsAtleastOneBorrowerExist(IEnumerable<SelectListItem> borrowers)
        {
            return borrowers.Count() > 0 ? true : false;
        }
        public bool IsAtleastOneBookExist(IEnumerable<SelectListItem> books)
        {
            return books.Count() > 0 ? true : false;
        }
        public bool IsAtleastOneBookAndOneBorrowerExist(DisplayBookBorrowerViewModel bbVM)
        {
            var books = bbVM.Books;
            var borrowers = bbVM.Borrowers;

            return (IsAtleastOneBookExist(books) && IsAtleastOneBorrowerExist(borrowers));
        }
    }
}