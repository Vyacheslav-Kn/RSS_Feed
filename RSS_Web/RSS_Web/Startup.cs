using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(RSS_Web.Startup))]
namespace RSS_Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
