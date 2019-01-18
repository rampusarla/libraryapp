using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using MonashDemo.Core.ViewModels;

namespace MonashDemo.Core.Models
{    
    public class Book : BookViewModel
    {
        public bool IsBorrowed;
        
        [DataType(DataType.Date)]
        [Display(Name = "Date Borrowed")]        
        public DateTime DateBorrowed;
    }
}