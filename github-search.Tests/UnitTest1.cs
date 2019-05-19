using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using github_search.Core.Consts;
using github_search.Services;
using github_search.Services.Interfaces;
using github_search.ViewModels.Repository;
using github_search.ViewModels.User;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NUnit.Framework;

namespace github_search.Tests
{
    [TestFixture]
    class Test_GithubApiService
    {

        private readonly GithubApiService _primeService;

        public Test_GithubApiService()
        {
            _primeService = new GithubApiService();
        }
     
        #region BuildURL
        [TestCase]
        public void GithubApiService_UserDetailedRequest_BuildURL()
        {
            var name = "DanielPaulWilkinson";

            var result = _primeService.BuildURL(name, Core.GitHubRequestTypeEnum.UserDetailedRequest);

            var expected = string.Format(GithubCalls.searchUserAccountTemp, name);

            NUnit.Framework.Assert.AreEqual(result, expected);
        }
        [TestCase]
        public void GithubApiService_UserRepoRequest_BuildURL()
        {
            var name = "DanielPaulWilkinson";

            var result = _primeService.BuildURL(name, Core.GitHubRequestTypeEnum.UserRepoRequest);

            var expected = string.Format(GithubCalls.searchRepoTemp, name);

            NUnit.Framework.Assert.AreEqual(result, expected);
        }
        [TestCase]
        public void GithubApiService_UserRequest_BuildURL()
        {
            var name = "DanielPaulWilkinson";

            var result = _primeService.BuildURL(name, Core.GitHubRequestTypeEnum.UserRequest);

            var expected = string.Format(GithubCalls.searchUsersTemp, name);

            NUnit.Framework.Assert.AreEqual(result, expected);
        }
        [TestCase]
        public void GithubApiService_UserRepoRequest_UserRequestTemplate_BuildURL()
        {
            var name = "DanielPaulWilkinson";

            var result = _primeService.BuildURL(name, Core.GitHubRequestTypeEnum.UserRepoRequest);

            var expected = string.Format(GithubCalls.searchUsersTemp, name);

            NUnit.Framework.Assert.AreNotEqual(result, expected);
        }
        #endregion
        #region RepoURL
        [TestCase]
        public async Task GithubApiService_GetRepositoriesByURL_CorrectType()
        {
            var name = "DanielPaulWilkinson";

            var result = await _primeService.GetRepositoriesByURL(typeof(List<GithubRepo>),string.Format(GithubCalls.searchRepoTemp, name));

            NUnit.Framework.Assert.NotNull(result);
        }
        [TestCase]
        public async Task GithubApiService_GetRepositoriesByURL_IncorrectType()
        {
            //tests to see if the incorrect type throws an error
            var name = "DanielPaulWilkinson";

            bool success = false;

            try
            {
                var result = await _primeService.GetRepositoriesByURL(typeof(GithubRepo), string.Format(GithubCalls.searchRepoTemp, name));
                success = true;
            }
            catch
            {
                success = false;
            }

            NUnit.Framework.Assert.False(success);

        }
        #endregion

        [TestCase]
        public async Task GithubApiService_GetUsersByName_GithubDetailedUser()
        {
            //tests to see if the incorrect type throws an error
            var name = "DanielPaulWilkinson";

            bool success = false;

            try
            {
                var result = await _primeService.GetUsersByName(typeof(GithubDetailedUser), string.Format(GithubCalls.searchUserAccountTemp, name), Core.GitHubRequestTypeEnum.UserDetailedRequest);
                success = true;
            }
            catch
            {
                success = false;
            }

            NUnit.Framework.Assert.True(success);

        }
        [TestCase]
        public async Task GithubApiService_GetUsersByName_GithubBaseUser()
        {
            //tests to see if the incorrect type throws an error
            var name = "";

            bool success = false;

            try
            {
                var result = await _primeService.GetUsersByName(typeof(GithubBaseUser), string.Format(GithubCalls.searchUsersTemp, name), Core.GitHubRequestTypeEnum.UserRequest);
                success = true;
            }
            catch
            {
                success = false;
            }

            NUnit.Framework.Assert.True(success);
        }
        [TestCase]
        public async Task GithubApiService_GetUsersByName_GithubRepo()
        {
            //tests to see if the incorrect type throws an error
            var name = "";

            bool success = false;

            try
            {
                var result = await _primeService.GetUsersByName(typeof(List<GithubRepo>), string.Format(GithubCalls.searchRepoTemp, name), Core.GitHubRequestTypeEnum.UserRepoRequest);
                success = true;
            }
            catch
            {
                success = false;
            }

            NUnit.Framework.Assert.True(success);
        }
    }
}
