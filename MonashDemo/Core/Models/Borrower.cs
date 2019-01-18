using MonashDemo.Core.Errors;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MonashDemo.Core.ViewModels;

namespace MonashDemo.Core.Models
{
    public class Borrower
    {
        public int BorrowerId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50)]        
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50)]        
        public string LastName { get; set; }        
    }
}