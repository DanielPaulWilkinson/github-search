using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels.Pager
{
    public class PagerVM
    {
        public string BaseUrl { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public int PreviousPage { get; set; }
    }
}
