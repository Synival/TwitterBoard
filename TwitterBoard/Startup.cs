using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TwitterBoard.Startup))]
namespace TwitterBoard
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
