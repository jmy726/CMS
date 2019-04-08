using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CMS2.Startup))]
namespace CMS2
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
