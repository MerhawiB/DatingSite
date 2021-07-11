using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dejtinghemsida.Startup))]
namespace Dejtinghemsida
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
