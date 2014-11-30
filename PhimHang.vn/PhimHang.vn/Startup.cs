using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(PhimHang.vn.Startup))]
namespace PhimHang.vn
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
