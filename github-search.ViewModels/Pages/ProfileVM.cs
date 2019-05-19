using github_search.ViewModels.Repo;
using github_search.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels
{
    public class ProfileVM
    {
        public List<GithubRepo> Repositories { get; set; }
        public GithubDetailedUser User { get; set; }
    }
}
