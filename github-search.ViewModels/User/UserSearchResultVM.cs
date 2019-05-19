using System.Collections.Generic;

namespace github_search.ViewModels.User
{
    public class UserSearchResultVM
    {
        public int total_count { get; set; }
        public bool incomplete_results { get; set; }
        public List<GithubBaseUser> items { get; set; }
    }
}
