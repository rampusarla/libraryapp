using MonashDemo.Core.Models;
using MonashDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MonashDemo.Core.Interfaces
{
    public interface IBookService
    {
        bool IsAtleastOneOverdueBookExist();
        IEnumerable<Book> GetAllOverdueBooks();
        bool CreateBook(BookViewModel bookVM);
        IEnumerable<Book> GetAllBooks(string sortOrder, string searchString);
    }
}
