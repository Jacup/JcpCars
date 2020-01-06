using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(JCPCars.Startup))]
namespace JCPCars
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
