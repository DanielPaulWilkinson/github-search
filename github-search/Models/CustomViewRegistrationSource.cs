using Autofac;
using Autofac.Builder;
using Autofac.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace github_search.ViewModels
{
    public class CustomViewRegistrationSource : IRegistrationSource
    {
        public IEnumerable<IComponentRegistration> RegistrationsFor(Service service, Func<Service, IEnumerable<IComponentRegistration>> registrationAccessor)
        {
            var typedService = service as IServiceWithType;

            if (typedService != null && IsSupportedView(typedService.ServiceType))
            {
                yield return RegistrationBuilder.ForType(typedService.ServiceType)
                    .PropertiesAutowired()
                    .InstancePerRequest()
                    .CreateRegistration();
            }
        }

        public bool IsAdapterForIndividualComponents => false;

        private static bool IsSupportedView(Type serviceType)
        {
            return serviceType.IsAssignableTo<WebViewPage>()
                || serviceType.IsAssignableTo<ViewPage>()
                || serviceType.IsAssignableTo<ViewMasterPage>()
                || serviceType.IsAssignableTo<ViewUserControl>();
        }
    }
}
