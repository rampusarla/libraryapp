using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MonashDemo.Core.ViewModels
{
    public class DisplayBookBorrowerViewModel
    {
        public IEnumerable<SelectListItem> Borrowers { get; set; }
        public IEnumerable<SelectListItem> Books { get; set; }

        public int BookId { get; set; }
        public int BorrowerId { get; set; }

        [Display(Name = "List of Borrowers")]
        public string BorrowerNameHeader { get; set; }

        [Display(Name = "List of Available Books")]
        public string BookNameHeader { get; set; }
    }
}