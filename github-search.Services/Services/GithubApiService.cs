using github_search.Core;
using github_search.Core.Consts;
using github_search.Core.Helpers;
using github_search.Services.Interfaces;
using github_search.ViewModels;
using github_search.ViewModels.Repo;
using github_search.ViewModels.User;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;

namespace github_search.Services
{
    public class GithubApiService : IGithubApiService
    {
        #region Generic Requests
        public async Task<dynamic> GetUsersByName<T>(T t, string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum = GitHubRequestTypeEnum.UserRequest) where T : Type
        {
            var url = BuildURL(Name, gitHubRequestTypeEnum);

            var vm = await MakeRequest(t, url);

            return vm;
        }

        public async Task<ProfileVM> GetProfile(string name)
        {
            var vm = new ProfileVM();

            vm.User = await GetUsersByName(typeof(GithubDetailedUser), name, Core.GitHubRequestTypeEnum.UserDetailedRequest);

            if (vm.User != null && vm.User.repos_url != null && vm.User.repos_url.Length > 0)
            {
                vm.Repositories = await GetRepositoriesByURL(typeof(List<GithubRepo>), vm.User.repos_url);
            }

            return vm;
        }

        public async Task<List<GithubRepo>> GetRepositoriesByURL<T>(T t, string repos_url) where T : Type
        {
            var vm = await MakeRequest(t, repos_url);

            return vm;
        }
        #endregion

        #region Request and Helpers
        private async Task<dynamic> MakeRequest<T>(T t, string url) where T : Type
        {
            var vm = GetObject(t);

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

                        if (t == typeof(UserSearchResultVM))
                        {
                            vm = JsonConvert.DeserializeObject<UserSearchResultVM>(reader);
                        }
                        else if (t == typeof(List<GithubRepo>))
                        {
                            vm = JsonConvert.DeserializeObject<List<GithubRepo>>(reader);
                        }
                        else if (t == typeof(GithubDetailedUser))
                        {
                            vm = JsonConvert.DeserializeObject<GithubDetailedUser>(reader);
                        }
                    }
                }
                catch (Exception e)
                {
                    vm = null;
                }

            }

            return vm;
        }

        private string BuildURL(string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum)
        {
            var request = "";

            switch (gitHubRequestTypeEnum)
            {
                case GitHubRequestTypeEnum.UserRequest:
                    request = string.Format(GithubCalls.searchUsersTemp, Name);
                    break;

                case GitHubRequestTypeEnum.UserDetailedRequest:
                    request = string.Format(GithubCalls.searchUserAccountTemp, Name);
                    break;
                case GitHubRequestTypeEnum.UserRepoRequest:
                    request = string.Format(GithubCalls.searchRepoTemp, Name);
                    break;
            }

            return request;
        }

        private dynamic GetObject(Type t)
        {
            if (t == typeof(UserSearchResultVM)) { return new UserSearchResultVM(); }
            else if (t == typeof(GithubBaseUser)) { return new GithubBaseUser(); }
            else if (t == typeof(List<GithubRepo>)) { return new List<GithubRepo>(); }
            else if (t == typeof(GithubDetailedUser)) { return new GithubDetailedUser(); }
            else { throw new Exception($"No object type here for {t.Name}"); };
        }
        #endregion
    }
}
