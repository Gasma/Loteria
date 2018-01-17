using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CasaDeJogos.Startup))]
namespace CasaDeJogos
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //ConfigureAuth(app);
        }
    }
}
