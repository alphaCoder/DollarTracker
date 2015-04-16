using System.Web;
using System.Web.Optimization;

namespace DollarTracker.Web
{
	public class BundleConfig
	{
		// For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
						"~/Scripts/jquery-{version}.js"));

			bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
						"~/Scripts/jquery.validate*"));

			// Use the development version of Modernizr to develop with and learn from. Then, when you're
			// ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
			bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
						"~/Scripts/modernizr-*"));

			bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
					  "~/Scripts/bootstrap.js",
					  "~/Scripts/respond.js"));

			bundles.Add(new StyleBundle("~/Content/css").Include(
					  "~/Content/bootstrap.css"));//,
					  //"~/Content/site.css"));
			//bundles.Add(new StyleBundle("~/bundles/adminLTE").Include(
			//	"~/AdminLTE/css/AdminLTE.min.css",	
			//	"~/AdminLTE/css/skins/skin-blue.min.css"
			//	));

			bundles.Add(new ScriptBundle("~/bundles/angular").Include(
				"~/Scripts/angular.js",
				"~/Scripts/angular-ui-router.js",
				"~/Scripts/angular-mocks.js",
				"~/Scripts/angular-ui/ui-bootstrap.js",
				"~/Scripts/angular-ui/ui-bootstrap-tpls.min.js"
				));
			bundles.Add(new ScriptBundle("~/app/all").IncludeDirectory("~/app", "*.js", true));
			bundles.Add(new ScriptBundle("~/adminLTE/all").Include(
				"~/AdminLTE/js/app.min.js"
				));


		}
	}
}
