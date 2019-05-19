using github_search.Core;
using github_search.ViewModels.Pages;
using github_search.ViewModels.Repository;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace github_search.Services.Interfaces
{
    public interface IGithubApiService : IBaseService
    {
        Task<dynamic> GetUsersByName<T>(T t, string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum = GitHubRequestTypeEnum.UserRequest) where T : Type;
        Task<List<GithubRepo>> GetRepositoriesByURL<T>(T t, string repos_url) where T : Type;
        Task<ProfileVM> GetProfile(string name);
        string BuildURL(string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum);
    }
}
