using github_search.Services.Interfaces;
using github_search.ViewModels.Pages;
using github_search.ViewModels.User;
using System;
using System.IO;
using System.Threading.Tasks;
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

        #region Home
        public ActionResult Index()
        {
            var vm = new HomeVM();
            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> Search(string search)
        {
            var vm = new HomeVM
            {
                Search = search,
                Results = await _githubApiService.GetUsersByName(typeof(UserSearchResultVM), search),
                User = await _githubApiService.GetUsersByName(typeof(GithubDetailedUser), search, Core.GitHubRequestTypeEnum.UserDetailedRequest),
            };

            if (vm.User != null)
            {
                return RedirectToAction("LearnMore", new { login = vm.User.login });
            }

            return View("index", vm);
        }
        public async Task<ActionResult> LearnMore(string login)
        {
            //Since it was a requirement that the 'repo_url' was used from the first call, I have commented this out and have used the below method instead.
            //var vm = new ProfileVM
            //{
            //    User = await _githubApiService.GetUsersByName(typeof(GithubDetailedUser), name, Core.GitHubRequestTypeEnum.UserDetailedRequest),
            //    Repositories  = await _githubApiService.GetUsersByName(typeof(List<GithubRepo>), name, Core.GitHubRequestTypeEnum.UserRepoRequest),
            //};

            var vm = await _githubApiService.GetProfile(login);

            if (vm.User != null)
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
        #endregion

        #region Home Private Methods
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
        #endregion

    }
}