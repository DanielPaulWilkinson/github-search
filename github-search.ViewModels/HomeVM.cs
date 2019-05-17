using github_search.ViewModels.Pager;
using github_search.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels
{
    public class HomeVM : BaseVM
    {
        public string Search { get; set; }
        public SearchResultVM Results { get; set;}
        public PagerVM Pager { get; set; }
    }
}
