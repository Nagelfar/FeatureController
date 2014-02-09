using Castle.MicroKernel.Registration;
using Microsoft.AspNet.SignalR.Hubs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FeatureController.Infrastructure.Installers
{
    public class HubInstaller:IWindsorInstaller
    {
        public void Install(Castle.Windsor.IWindsorContainer container, Castle.MicroKernel.SubSystems.Configuration.IConfigurationStore store)
        {
             container.Register(Classes.FromThisAssembly().BasedOn(typeof(IHub)).LifestyleTransient()); 
    //        container.Register(
    //            Component.For<IHubConnectionContext>()
    //                .UsingFactoryMethod(kernel =>
    //    kernel.Resolve<IConnectionManager>().GetHubContext<StockTickerHub>().Clients
    //)
    //             Component.For<IHubConnectionContext>().
    //                ToMethod(context =>
    //            resolver.Resolve<IConnectionManager>().
    //                GetHubContext<StockTickerHub>().Clients
    //        ).WhenInjectedInto<IStockTicker>();
    //            );
        }
    }
}