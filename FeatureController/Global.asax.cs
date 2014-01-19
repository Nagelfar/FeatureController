using Castle.Windsor;
using FeatureController.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

using System.IO;
namespace FeatureController
{
    // Note: For instructions on enabling IIS7 classic mode, 
    // visit http://go.microsoft.com/fwlink/?LinkId=301868
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {            
            Bootstrapper.Bootstrap()
                .InitFeatures()
                .InitMvc()
                .FeatureizeMvc()
                .Boot();

        }
    }
}
