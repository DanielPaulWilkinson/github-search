using github_search.ViewModels.Base;
using github_search.ViewModels.User;

namespace github_search.ViewModels.Pages
{
    public class HomeVM : BaseVM
    {
        public string Search { get; set; }
        public GithubDetailedUser User  { get; set; }
        public UserSearchResultVM Results { get; set;}
    }
}
