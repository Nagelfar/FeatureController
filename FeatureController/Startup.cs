using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FeatureController.Startup))]
namespace FeatureController
{
    public partial class Startup 
    {
        public void Configuration(IAppBuilder app) 
        {
            ConfigureAuth(app);
        }
    }
}
