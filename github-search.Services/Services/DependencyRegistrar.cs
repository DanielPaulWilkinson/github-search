using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using github_search.Core.Interfaces;
using github_search.Services.Interfaces;

namespace github_search.Services
{
    public class DependencyRegistrar : IDependencyRegistrar
    {
        public void Register(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => t.GetInterfaces().Any(i => i.IsAssignableFrom(typeof(IBaseService))))
                .AsImplementedInterfaces()
                .As<IBaseService>();
        }
    }
}