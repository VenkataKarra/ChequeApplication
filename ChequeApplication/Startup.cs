using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ChequeApplication.Startup))]
namespace ChequeApplication
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
