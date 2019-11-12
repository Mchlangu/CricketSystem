using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CricketSystem.Startup))]
namespace CricketSystem
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
