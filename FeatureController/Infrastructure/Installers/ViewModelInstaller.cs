using Castle.MicroKernel.Registration;
using FeatureController.Features.Foo.Projections;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Infrastructure.Installers
{
    public class ViewModelInstaller:IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For < FooProjection>()
                    .LifestyleSingleton()
                );
        }
    }
}