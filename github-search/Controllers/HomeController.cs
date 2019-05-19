using github_search.Services.Interfaces;
using github_search.ViewModels;
using github_search.ViewModels.Repo;
using github_search.ViewModels.User;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace github_search.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IGithubApiService _githubApiService;
        private readonly IPdfService _pdfService;

        public HomeController(IGithubApiService githubApiService, IPdfService pdfService)
        {
            _githubApiService = githubApiService;
            _pdfService = pdfService;
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
            if (vm.User != null)
            {
                return RedirectToAction("LearnMore", new { name = vm.User.login });
            }

            return View("index", vm);
        }

        public async Task<ActionResult> LearnMore(string name)
        {
            //Since it was requested in the breif that a seperate repo url be called as a result of the inital, instead of a generic call a seperate method was creaated that takes a repo url.
            //var vm = new ProfileVM
            //{
            //    User = await _githubApiService.GetUsersByName(typeof(GithubDetailedUser), name, Core.GitHubRequestTypeEnum.UserDetailedRequest),
            //    Repositories  = await _githubApiService.GetUsersByName(typeof(List<GithubRepo>), name, Core.GitHubRequestTypeEnum.UserRepoRequest),
            //};

            var vm = await _githubApiService.GetProfile(name);

            if(vm.User != null)
            {
                return View("profile", vm);
            }
            else
            {
                Throw404();
            }

            return View("profile", vm);
        }

        public async Task<ActionResult> Download(string login)
        {
            var vm = await _githubApiService.GetProfile(login);

            var memoryStream = _pdfService.GetProfile(vm);

            //Get the pointer to the beginning of the stream. 
            DownloadAsPDF(memoryStream, $"{DateTime.Now.ToString("yyyyMMddHHmmss")}-{vm.User.login}");
            return RedirectToAction("LearnMore", new { name = login });
        }
        private void DownloadAsPDF(MemoryStream ms, string filename)
        {
            Response.Clear();
            Response.ClearContent();
            Response.ClearHeaders();
            Response.ContentType = "application/pdf";
            Response.AppendHeader("Content-Disposition", $"attachment;filename={filename}.pdf");
            Response.OutputStream.Write(ms.GetBuffer(), 0, ms.GetBuffer().Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.End();
            ms.Close();
        }
    }
}