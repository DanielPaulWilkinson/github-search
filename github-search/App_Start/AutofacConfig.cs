using Autofac;
using github_search.Core.Interfaces;
using github_search.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using Autofac.Integration.Mvc;

namespace github_search.App_Start
{
    public class AutofacConfig
    {
        public static void Register()
        {
            try
            {
                var builder = new ContainerBuilder();

                builder.RegisterControllers(typeof(MvcApplication).Assembly);

                builder.RegisterSource(new CustomViewRegistrationSource());

                var registrars = GetAssemblies()
                    .SelectMany(a => a.GetTypes())
                    .Where(t => t.GetInterfaces().Any(i => i == typeof(IDependencyRegistrar)))
                    .Select(t => (IDependencyRegistrar)Activator.CreateInstance(t))
                    .ToList();

                foreach (var registrar in registrars)
                {
                    registrar.Register(builder);
                }

                builder.RegisterModule<AutofacWebTypesModule>();

                var container = builder.Build();
                var mvcResolver = new AutofacDependencyResolver(container);
                DependencyResolver.SetResolver(mvcResolver);
            }
            catch (ReflectionTypeLoadException ex)
            {
                StringBuilder sb = new StringBuilder();
                foreach (Exception exSub in ex.LoaderExceptions)
                {
                    sb.AppendLine(exSub.Message);
                    FileNotFoundException exFileNotFound = exSub as FileNotFoundException;
                    if (exFileNotFound != null)
                    {
                        if (!string.IsNullOrEmpty(exFileNotFound.FusionLog))
                        {
                            sb.AppendLine("Fusion Log:");
                            sb.AppendLine(exFileNotFound.FusionLog);
                        }
                    }
                    sb.AppendLine();
                }
                string errorMessage = sb.ToString();
            }
        }

        private static IEnumerable<Assembly> GetAssemblies()
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies().ToList();

            foreach (var dll in Directory.GetFiles(GetBinDirectory(), "*.dll"))
            {
                try
                {
                    var assemblyName = AssemblyName.GetAssemblyName(dll);

                    if (assemblies.Any(a => a.FullName == assemblyName.FullName)) continue;

                    var assembly = AppDomain.CurrentDomain.Load(assemblyName);
                    assemblies.Add(assembly);
                }
                catch (BadImageFormatException)
                {
                }
            }

            return assemblies;
        }

        private static string GetBinDirectory()
        {
            return HostingEnvironment.IsHosted ?
                HttpRuntime.BinDirectory : AppDomain.CurrentDomain.BaseDirectory;
        }
    }
}