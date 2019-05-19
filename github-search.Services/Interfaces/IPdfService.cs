using github_search.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace github_search.Services.Interfaces
{
    public interface IPdfService : IBaseService
    {
        MemoryStream GetProfile(ProfileVM vm);
    }
}
