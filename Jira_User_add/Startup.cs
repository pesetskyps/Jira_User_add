using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Jira_User_add.Startup))]
namespace Jira_User_add
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
