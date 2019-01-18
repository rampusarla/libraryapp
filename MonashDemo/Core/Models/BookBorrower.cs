using MonashDemo.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonashDemo.Core.Models
{
    public class BookBorrower : AddBookBorrowerViewModel
    {        
        [Display(Name = "Date Borrowed")]
        public DateTime DateBorrowed { get; set; }

        [Display(Name = "Date Borrowed")]
        public DateTime DateReturned { get; set; }
    }
    
}    