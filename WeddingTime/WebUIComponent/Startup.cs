using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AIT.WebUIComponent.Startup))]
namespace AIT.WebUIComponent
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
