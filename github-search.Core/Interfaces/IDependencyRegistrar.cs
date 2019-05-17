using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;

namespace github_search.Core.Interfaces
{
    public interface IDependencyRegistrar
    {
        void Register(ContainerBuilder builder);
    }
}
