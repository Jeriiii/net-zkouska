using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(netZkouska.Startup))]
namespace netZkouska
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
