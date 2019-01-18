using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MonashDemo.Core.ViewModels
{
    public class BookViewModel
    {
        [Required]
        public int BookId { get; set; }
        
        [Required]
        [Display(Name = "Book Title")]
        [StringLength(50)]        
        public string Title { get; set; }

        [Required]
        [Display(Name = "Book Author")]
        [StringLength(50)]
        public string Author { get; set; }        
    }
}