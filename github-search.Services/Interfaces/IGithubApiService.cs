using github_search.Core;
using github_search.ViewModels;
using github_search.ViewModels.Repo;
using github_search.ViewModels.User;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.Services.Interfaces
{
    public interface IGithubApiService : IBaseService
    {
        Task<dynamic> GetUsersByName<T>(T t, string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum = GitHubRequestTypeEnum.UserRequest) where T : Type;
        Task<List<GithubRepo>> GetRepositoriesByURL<T>(T t, string repos_url) where T : Type;
        Task<ProfileVM> GetProfile(string name);
    }
}
