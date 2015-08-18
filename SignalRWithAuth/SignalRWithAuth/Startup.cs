using Microsoft.AspNet.SignalR;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SignalRWithAuth.Startup))]
namespace SignalRWithAuth
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
           
          //  GlobalHost.HubPipeline.RequireAuthentication();
        }
    }
}
