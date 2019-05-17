using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.ViewModels
{
    public class OwnerVM : BaseVM
    {
        public string login { get; set; }
        public string node_id { get; set; }
        public string avatar_url { get; set; }
        public string gravatar_id { get; set; }
        public string url { get; set; }
        public string received_events_url { get; set; }
        public string type { get; set; }
    }
}
