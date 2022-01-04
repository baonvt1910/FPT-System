using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(FPTLearningManagement.Startup))]
namespace FPTLearningManagement
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
