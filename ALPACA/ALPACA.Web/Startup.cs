using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ALPACA.Web.Startup))]
namespace ALPACA.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
