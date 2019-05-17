using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels.User
{
    public class SearchResultVM
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<GithubUser> items { get; set; }
    }
}
