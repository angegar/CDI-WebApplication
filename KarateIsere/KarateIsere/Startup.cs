using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(KarateIsere.Startup))]
namespace KarateIsere
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
