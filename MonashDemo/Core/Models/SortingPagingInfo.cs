using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonashDemo.Core.Models
{
    public class SortingPagingInfo
    {
        public string SortField { get; set; }
        public string SortOrder { get; set; }        
        public int PageSize { get; set; }
        public int PageCount { get; set; }
        public int CurrentPageIndex { get; set; }
    }
}