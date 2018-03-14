using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TGWLogs.Startup))]
namespace TGWLogs
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
