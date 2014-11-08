using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ZippyJobs.Web.Startup))]
namespace ZippyJobs.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
