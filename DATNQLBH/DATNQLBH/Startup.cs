using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DATNQLBH.Startup))]
namespace DATNQLBH
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
