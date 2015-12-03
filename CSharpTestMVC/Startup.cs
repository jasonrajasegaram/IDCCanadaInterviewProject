using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(CSharpTestMVC.Startup))]
namespace CSharpTestMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            
        }
    }
}
