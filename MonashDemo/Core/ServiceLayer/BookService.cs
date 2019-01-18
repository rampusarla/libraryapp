using MonashDemo.Core.Errors;
using MonashDemo.Core.Interfaces;
using MonashDemo.Core.Models;
using MonashDemo.Core.Repositories;
using MonashDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonashDemo.Core.ServiceLayer
{
    public class BookService : IBookService
    {
        private IValidationDictionary _validatonDictionary;
        private IBookRepository _repository;
        

        public BookService(IValidationDictionary validationDictionary, IBookRepository repository)
        {
            _validatonDictionary = validationDictionary;
            _repository = repository;
        }

        public bool IsAtleastOneOverdueBookExist()
        {
            return GetAllOverdueBooks().Count() > 0 ? true : false;
        }
        public IEnumerable<Book> GetAllOverdueBooks()
        {
            return _repository.GetAllOverdueBooks();
        }

        public bool CreateBook(BookViewModel bookVM)
        {
            if (_repository.IsBookAlreadyExists(bookVM))
                _validatonDictionary.AddError(ErrorMessages.BookAlreadyExistsHeading, ErrorMessages.BookAlreadyExistsMessage);

            if (_validatonDictionary.IsValid)
            {
                _repository.AddBook(bookVM);
                return true;
            }
            else return false;
        }

        public IEnumerable<Book> GetAllBooks(string sortOrder, string searchString)
        {
            return _repository.GetAllBooks(sortOrder, searchString).ToList();
        }
    }
}
