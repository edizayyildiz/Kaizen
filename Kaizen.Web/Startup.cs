using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Kaizen.Web.Startup))]
namespace Kaizen.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
