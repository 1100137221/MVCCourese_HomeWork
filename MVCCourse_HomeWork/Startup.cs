using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVCCourse_HomeWork.Startup))]
namespace MVCCourse_HomeWork
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
