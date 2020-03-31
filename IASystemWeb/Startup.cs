using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IASystemWeb.Startup))]
namespace IASystemWeb
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
