using Caliburn.Micro;
using Ravlyk.Services;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Ravlyk.iOS
{
    public class CaliburnAppDelegate : CaliburnApplicationDelegate
    {
        private SimpleContainer container;

        public CaliburnAppDelegate()
        {
            Initialize();
        }

        protected override void Configure()
        {
            container = new SimpleContainer();
            container.PerRequest<IPlatformService, PlatformService>();
            container.Instance(container);
        }

        protected override void BuildUp(object instance)
        {
            container.BuildUp(instance);
        }

        protected override IEnumerable<object> GetAllInstances(Type service)
        {
            return container.GetAllInstances(service);
        }

        protected override object GetInstance(Type service, string key)
        {
            return container.GetInstance(service, key);
        }

       
    }
}
