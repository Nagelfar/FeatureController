﻿using Castle.MicroKernel.Registration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using Castle.Facilities.TypedFactory;
namespace FeatureController.Infrastructure.Installers
{
    public class MediatorQueryInstaller:IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
            container.Register(
                Component.For<IMediator>()
                    .ImplementedBy < Mediator>(),

                Component.For<Mediator.IFindQueryHandlers>()
                    .AsFactory(new TypeDFactoryMediatorSelector() ),

                Classes.FromAssemblyInThisApplication()
                    .BasedOn(typeof(IQueryHandler<,>))
                    //.WithService.Select(
                    //    (x,y)=>y
                    //)
                    .WithServiceAllInterfaces()
                    .LifestyleTransient()
                    );
        }

        private class TypeDFactoryMediatorSelector : DefaultTypedFactoryComponentSelector
        {
            //protected override string GetComponentName(System.Reflection.MethodInfo method, object[] arguments)
            //{
            //    var name= base.GetComponentName(method, arguments);
            //    return name;
            //}
            protected override Type GetComponentType(System.Reflection.MethodInfo method, object[] arguments)
            {
                var type= base.GetComponentType(method, arguments);

               var newtype= typeof(IQueryHandler<,>)
                    .MakeGenericType(
                        arguments[0].GetType(),
                        method.GetGenericArguments()[0]
                    );
                return newtype;
            }
            //protected override Func<Castle.MicroKernel.IKernelInternal, Castle.MicroKernel.IReleasePolicy, object> BuildFactoryComponent(System.Reflection.MethodInfo method, string componentName, Type componentType, System.Collections.IDictionary additionalArguments)
            //{
            //    //var c = base.BuildFactoryComponent(method, componentName, componentType, additionalArguments);
            //    //return c;

            //    return (kernel, release) => kernel.Resolve(componentType);
            //}
        }
    }
}