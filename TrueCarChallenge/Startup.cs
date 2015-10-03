using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TrueCarChallenge.Startup))]
namespace TrueCarChallenge
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
