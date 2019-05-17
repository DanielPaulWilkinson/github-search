using github_search.Services.Interfaces;
using github_search.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace github_search.Controllers
{
    public class HomeController : Controller
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
                Results = await _githubApiService.GetUsersByName(Search),
            };

            return View("index",vm);
        }
    }
}