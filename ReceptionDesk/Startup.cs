using System.Data.Entity;
using System.Web.Routing;
using Microsoft.Owin;
using Owin;
using ReceptionDesk.Models;

[assembly: OwinStartupAttribute(typeof(ReceptionDesk.Startup))]
namespace ReceptionDesk
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);

            // todo : 最終的にはマイグレーションを使用する
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<MyDbContext>());

            app.MapSignalR();
        }
    }
}
