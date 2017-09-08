using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpWebMvc.Startup))]
namespace CSharpWebMvc
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
