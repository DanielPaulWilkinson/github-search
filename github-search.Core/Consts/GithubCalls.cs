using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.Core.Consts
{
    public static class GithubCalls
    {
        //base
        public static string user = "users";
        public static string search = "search";
        public static string repo = "repos";

        public static string githubApiDomain = "https://api.github.com";
        public static string githubDomain  = "https://github.com";

        //search template 
        public static string searchUsersTemp = $"{githubApiDomain}/{search}/{user}?q="+"{0}";

        //user profile template
        public static string profile = $"{githubDomain}/" + "{0}";

        //exact account template 
        public static string searchUserAccountTemp = $"{githubApiDomain}/{user}/" + "{0}";

        //repo template
        public static string searchRepoTemp = $"{githubApiDomain}/{user}/"+"{0}"+$"/{repo}";
    }
}
