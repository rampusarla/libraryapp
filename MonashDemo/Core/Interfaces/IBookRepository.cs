using System;
using System.Collections.Generic;
using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;

namespace MonashDemo.Core.Interfaces
{
    public interface IBookRepository
    {
        void AddBook(BookViewModel bookVM);
        IEnumerable<Book> GetAllBooks(string sortOrder, string searchString);
        IEnumerable<Book> GetAllOverdueBooks();
        bool IsBookAlreadyExists(BookViewModel bookVM);
    }
}
