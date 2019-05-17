using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels.Repo
{
    public class GithubUser : BaseVM
    {
        public string node_id { get; set; }
        public string name { get; set; }
        public string full_name { get; set; }
        public OwnerVM owner { get; set; }
        public string html_url { get; set; }
        public string description { get; set; }
        public bool fork { get; set; }
        public string url { get; set; }
        public string created_at { get; set; }
        public string updated_at { get; set; }
        public string pushed_at { get; set; }
        public string homepage { get; set; }
        public int size { get; set; }
        public int stargazers_count { get; set; }
        public string watchers_count { get; set; }
        public string language { get; set; }
        public int forks_count { get; set; }
        public int open_issues_count { get; set; }
        public string master_branch { get; set; }
        public string default_branch { get; set; }
        public decimal score { get; set; }
    }
}
