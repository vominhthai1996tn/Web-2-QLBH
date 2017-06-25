using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebDoAn.Startup))]
namespace WebDoAn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
