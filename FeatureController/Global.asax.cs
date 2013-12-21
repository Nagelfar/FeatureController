using Castle.Windsor;
using FeatureController.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace FeatureController
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/fwlink/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new FeatureViewLocationRazorViewEngine());

            Container = Bootstrapper.Bootstrap();

            System.Web.Mvc.ControllerBuilder.Current.SetControllerFactory(new MvcControllerFactory(Container));

            //System.Web.Mvc.ControllerBuilder.Current.SetControllerFactory(new MvcControllerFactory(Container));

        }

        public Castle.Windsor.IWindsorContainer Container { get; set; }

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
                return _container.Resolve(controllerType) as IController;
            }
            public override void ReleaseController(IController controller)
            {
                base.ReleaseController(controller);
                _container.Release(controller);
            }


        }

    }
}
