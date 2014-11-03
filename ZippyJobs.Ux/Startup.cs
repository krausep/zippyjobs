using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZippyJobs.Ux.Startup))]
namespace ZippyJobs.Ux
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
