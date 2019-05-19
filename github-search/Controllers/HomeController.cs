using github_search.Services.Interfaces;
using github_search.ViewModels;
using github_search.ViewModels.Repo;
using github_search.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace github_search.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGithubApiService _githubApiService;

        public HomeController(IGithubApiService githubApiService)
        {
            _githubApiService = githubApiService;
        }


        public ActionResult Index()
        {
            var vm = new HomeVM();
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string Search)
        {
            var vm = new HomeVM
            {
                Search = Search,
                Results = await _githubApiService.GetUsersByName(typeof(UserSearchResultVM), Search),
                User = await _githubApiService.GetUsersByName(typeof(GithubDetailedUser), Search, Core.GitHubRequestTypeEnum.UserDetailedRequest),
            };

            //if the search result contains the same user redirect to results page...
            if (vm.User != null )
            {
                 return RedirectToAction("LearnMore", new { name = vm.User.login });
            }

            return View("index",vm);
        }

        public async Task<ActionResult> LearnMore(string name)
        {
            var vm = new ProfileVM
            {
                User = await _githubApiService.GetUsersByName(typeof(GithubDetailedUser), name, Core.GitHubRequestTypeEnum.UserDetailedRequest),
                Repositories  = await _githubApiService.GetUsersByName(typeof(List<GithubRepo>), name, Core.GitHubRequestTypeEnum.UserRepoRequest),
            };

            //Must be that we have ran out of calls for the moment
            if(vm.User == null && vm.Repositories == null)
            {
                Throw404();
            }

            return View("profile", vm);
        }
    }
}