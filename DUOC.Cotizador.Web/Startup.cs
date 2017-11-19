using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DUOC.Cotizador.Web.Startup))]
namespace DUOC.Cotizador.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
