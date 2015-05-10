using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DollarTracker.Web.Startup))]
namespace DollarTracker.Web
{
	public partial class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			ConfigureAuth(app);
		}
	}
}
