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

namespace FeatureController.Infrastructure
{
    public class Bootstrapper
    {
        private readonly IWindsorContainer _container;

        private Bootstrapper(IWindsorContainer container)
        {
            _container = container;
        }

        public static Bootstrapper Bootstrap()
        {            
            var container = new WindsorContainer();
            container.AddFacility<TypedFactoryFacility>();

            return new Bootstrapper( container);
        }

        public Bootstrapper InitWindsor(){
            _container.Install(Castle.Windsor.Installer.FromAssembly.This());

            return this;
        }

        public Bootstrapper InitFeatures()
        {
            FeatureSwitcher.Configuration.Features
                .Are
                .ConfiguredBy.Custom(x => File.Exists(Path.Combine(HttpRuntime.AppDomainAppPath, "App_Data", x.Value)))
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

            return this;
        }

        public Bootstrapper FeatureizeMvc()
        {
            FeatureBundles.FindAndRegisterAllFeatureBundles(BundleTable.Bundles);

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


    }
}