using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonashDemo.Core.ViewModels
{    
    public class AddBookBorrowerViewModel
    {
        public int Id { get; set; }
        public int BookId { get; set; }
        public int BorrowerId { get; set; }        
    }
}