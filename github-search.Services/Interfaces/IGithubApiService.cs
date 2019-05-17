using github_search.Core;
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
        Task<SearchResultVM> GetUsersByName(string Name, GitHubRequestTypeEnum gitHubRequestTypeEnum = GitHubRequestTypeEnum.UserRequest);
    }
}
