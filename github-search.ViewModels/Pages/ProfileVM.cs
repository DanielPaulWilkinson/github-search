using github_search.ViewModels.Repository;
using github_search.ViewModels.User;
using System.Collections.Generic;


namespace github_search.ViewModels.Pages
{
    public class ProfileVM
    {
        public List<GithubRepo> Repositories { get; set; }
        public GithubDetailedUser User { get; set; }
    }
}
