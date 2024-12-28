using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ApnaDoctorPortal.Startup))]
namespace ApnaDoctorPortal
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
