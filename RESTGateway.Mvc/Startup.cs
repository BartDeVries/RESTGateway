using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RESTGateway.Mvc.Startup))]
namespace RESTGateway.Mvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
