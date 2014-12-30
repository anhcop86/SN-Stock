using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Microsoft.Owin.Security.Cookies;
using Microsoft.AspNet.Identity;

[assembly: OwinStartupAttribute(typeof(PhimHang.Startup))]
namespace PhimHang
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
            ConfigureAuth(app);
        }
    }

}
