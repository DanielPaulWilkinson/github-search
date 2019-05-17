using github_search.Core;
using github_search.Core.Consts;
using github_search.Services.Interfaces;
using github_search.ViewModels;
using github_search.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace github_search.Services
{
    public class GithubApiService : IGithubApiService
    {
        public async Task<SearchResultVM> GetUsersByName(string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum = GitHubRequestTypeEnum.UserRequest)
        {
            var vm = new SearchResultVM();

            var url = GetGithubRequestType(Name, gitHubRequestTypeEnum);

            HttpWebRequest webRequest = WebRequest.Create(url) as HttpWebRequest;

            if (webRequest != null)
            {
                webRequest.Method = "GET";
                webRequest.UserAgent = "Anything";
                webRequest.ServicePoint.Expect100Continue = false;

                try
                {
                    using (StreamReader responseReader = new StreamReader(webRequest.GetResponse().GetResponseStream()))
                    {
                        string reader = await responseReader.ReadToEndAsync();
                        vm = JsonConvert.DeserializeObject<SearchResultVM>(reader);
                    }
                }
                catch(Exception e)
                {
                    vm =  null;
                }
            }

            return vm;
        }

        private string GetGithubRequestType(string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum)
        {
            var request = "";

            switch (gitHubRequestTypeEnum)
            {
                case GitHubRequestTypeEnum.UserRequest:
                    request = String.Format(GithubCalls.searchUsersTemp, Name);
                    break;
                case GitHubRequestTypeEnum.UserRepoRequest:
                    request = String.Format(GithubCalls.searchRepoTemp, Name);
                    break;
            }

            return request;
        }
    }
}
