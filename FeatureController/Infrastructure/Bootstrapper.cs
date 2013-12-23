using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.Startable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Infrastructure
{
    public class Bootstrapper
    {
        public static IWindsorContainer Bootstrap()
        {
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();

            container.Install(Castle.Windsor.Installer.FromAssembly.This());

            //var controllerFactory = new Castle.Windsor.Mvc.WindsorControllerFactory(container);
            return container;
        }
    }
}