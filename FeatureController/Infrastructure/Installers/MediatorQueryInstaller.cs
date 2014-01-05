using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using FeatureController.Common;
using Castle.Facilities.TypedFactory;
namespace FeatureController.Infrastructure.Installers
{
    public class MediatorQueryInstaller : IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IMediator>()
                    .ImplementedBy<Mediator>(),

                Component.For<Mediator.IFindQueryHandlers>()
                    .AsFactory(new TypeDFactoryMediatorSelector()),

                Classes.FromAssemblyInThisApplication()
                    .BasedOn(typeof(IQueryHandler<,>))
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    );
        }

        private class TypeDFactoryMediatorSelector : DefaultTypedFactoryComponentSelector
        {

            protected override Type GetComponentType(System.Reflection.MethodInfo method, object[] arguments)
            {
                var newtype = typeof(IQueryHandler<,>)
                     .MakeGenericType(
                         arguments[0].GetType(),
                         method.GetGenericArguments()[0]
                     );
                return newtype;
            }

        }
    }
}