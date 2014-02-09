using Castle.Windsor;
using Castle.Facilities.TypedFactory;
using Castle.Facilities.Startable;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using FeatureSwitcher.Configuration;
using FeatureSwitcher;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Optimization;
using FluentValidation.Mvc;
using Owin;
using FeatureController.Hubs;
using Microsoft.AspNet.SignalR;

namespace FeatureController.Infrastructure
{
    public class Bootstrapper
    {
        private readonly IWindsorContainer _container;
        private static Bootstrapper _bootstrapper;

        private Bootstrapper(IWindsorContainer container)
        {
            _container = container;
        }

        private static object Lock = new object();

        public static Bootstrapper Bootstrap()
        {
            lock (Lock)
            {
                if (_bootstrapper == null)
                {
                    var container = new WindsorContainer();
                    container.AddFacility<TypedFactoryFacility>();

                    container.Install(Castle.Windsor.Installer.FromAssembly.This());

                    _bootstrapper = new Bootstrapper(container);
                }
                return _bootstrapper;
            }
        }

        public Bootstrapper InitFeatures()
        {
            FeatureSwitcher.Configuration.Features
                .Are
                .ConfiguredBy.Custom(
                    x => File.Exists(Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", x.Value))
                )
                .NamedBy.TypeName()
                ;

            return this;
        }

        public void Boot()
        {
            
            // gogogogogo
        }

        public Bootstrapper InitMvc()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            System.Web.Mvc.ControllerBuilder.Current.SetControllerFactory(new MvcControllerFactory(_container));

            FluentValidationModelValidatorProvider.Configure(x =>
            {
                x.AddImplicitRequiredValidator = false;
            });

            return this;
        }

        public Bootstrapper FeatureizeMvc()
        {
            FeatureBundles.FindAndRegisterAllFeatureBundles(HttpContext.Current, BundleTable.Bundles, _container.ResolveAll<IController>());

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            return this;
        }


        private class MvcControllerFactory : DefaultControllerFactory
        {
            private readonly IWindsorContainer _container;
            private IDictionary<string, Type> _cache = new Dictionary<string, Type>();
            public MvcControllerFactory(IWindsorContainer container)
            {
                _container = container;
            }

            protected override IController GetControllerInstance(RequestContext requestContext, Type controllerType)
            {
                if (_container.Kernel.HasComponent(controllerType))
                    return _container.Resolve(controllerType) as IController;

                return base.GetControllerInstance(requestContext, controllerType);
            }
            public override void ReleaseController(IController controller)
            {
                base.ReleaseController(controller);
                _container.Release(controller);
            }


        }



        public Bootstrapper InitSignalR()
        {


            GlobalHost.DependencyResolver = new SignalR.Castle.Windsor.WindsorDependencyResolver(_container);
            
         

            NotificationHub.StartNotification();

            return this;
        }
    }
}